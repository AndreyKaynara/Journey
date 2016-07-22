/*
* The MIT License (MIT)
*
* Copyright (c) 2015-2016 Кайнара Андрей Витальевич
*
* Данная лицензия разрешает лицам, получившим копию данного программного обеспечения и  
* сопутствующей документации (в дальнейшем именуемыми «Программное Обеспечение»), 
* безвозмездно использовать Программное Обеспечение без ограничений, включая неограниченное 
* право на использование,  копирование, изменение, слияние, публикацию, распространение, 
* сублицензирование и/или продажу копий Программного Обеспечения,
* а также лицам, которым предоставляется данное Программное Обеспечение, при соблюдении следующих условий:
* 
* Указанное выше уведомление об авторском праве и данные условия должны быть включены во все копии или значимые части 
* данного Программного Обеспечения.
* 
* ДАННОЕ ПРОГРАММНОЕ ОБЕСПЕЧЕНИЕ ПРЕДОСТАВЛЯЕТСЯ «КАК ЕСТЬ», БЕЗ КАКИХ-ЛИБО ГАРАНТИЙ, ЯВНО ВЫРАЖЕННЫХ ИЛИ 
* ПОДРАЗУМЕВАЕМЫХ, ВКЛЮЧАЯ ГАРАНТИИ ТОВАРНОЙ ПРИГОДНОСТИ, СООТВЕТСТВИЯ ПО ЕГО КОНКРЕТНОМУ НАЗНАЧЕНИЮ И ОТСУТСТВИЯ НАРУШЕНИЙ, 
* НО НЕ ОГРАНИЧИВАЯСЬ ИМИ. НИ В КАКОМ СЛУЧАЕ АВТОРЫ ИЛИ ПРАВООБЛАДАТЕЛИ НЕ НЕСУТ ОТВЕТСТВЕННОСТИ ПО КАКИМ-ЛИБО ИСКАМ, 
* ЗА УЩЕРБ ИЛИ ПО ИНЫМ ТРЕБОВАНИЯМ, В ТОМ ЧИСЛЕ, ПРИ ДЕЙСТВИИ КОНТРАКТА, ДЕЛИКТЕ ИЛИ ИНОЙ СИТУАЦИИ, ВОЗНИКШИМ ИЗ-ЗА 
* ИСПОЛЬЗОВАНИЯ ПРОГРАММНОГО ОБЕСПЕЧЕНИЯ ИЛИ ИНЫХ ДЕЙСТВИЙ С ПРОГРАММНЫМ ОБЕСПЕЧЕНИЕМ.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JourneyTSP;
using JourneyLogs;

namespace JourneyToursComputing
{
    /// <summary>
    /// Вычисляет тур алгоритмом Лина-Кернигана. 
    /// Класс принимает данные типа NodesList, но оперирует с данными класса EdgesList.
    /// </summary>
    class LinKernighanSolver
    {
        //===============================================================================
        // Поля

        private NodesList nodesList; // Узлы, в которых содержится первоначальный тур. 
        private EdgesList edgesList; // Список рёбр, которые формируются на основе списка узлов. С ними совершаются основные манипуляции по ходу работы алгоритма. 
        private Stack<Edge> xEdges; // Список удалённых рёбр в виде стека.
        private Stack<Edge> yEdges; // Список добавленных рёбр в виде стека.
        private double totalGain; // Общая выгода от всех обменов по ходу работы решателя. 
        private double gainSum; // Сумма выгод от всех обменов на текущей серии обменов.
        // Параметры алгоритма.
        private int nearestNeighbors; // Число ближайших соседей, которые рассматриваются как кандидаты для ребра.
        private double gap; // Максимальная возможная разница от двух улучшений тура до прекращения расчётов.

        //===============================================================================
        // Свойства

        /// <summary>
        /// Общая выгода от всех сделанных обменов по ходу работы решателя. 
        /// </summary>
        public double TotalGain
        {
            get { return totalGain; }
        }

        //===============================================================================
        // Методы  

        // Private

        /// <summary>
        /// Проверяет, есть ли в стеке указанное ребро. Порядок вершин в ребре значения не имеет. 
        /// </summary>
        private bool IsInStack(Stack<Edge> stack, Edge edge)
        {
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack.ElementAt(i).IsEqual(edge))
                    return true;
            }
            return false;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Проверяет, есть ли в стеке ребро с переданными вершинами. Порядок вершин в ребре значений не имеет. 
        /// </summary>
        private bool IsInStack(Stack<Edge> stack, Node a, Node b)
        {
            for (int i = 0; i < stack.Count; i++)
            {
                if (stack.ElementAt(i).IsEqual(a, b))
                    return true;
            }
            return false;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Отменяет последний обмен на списке рёбер. 
        /// </summary>
        private void UndoReplace()
        {
            if ((xEdges.Count != yEdges.Count) || (xEdges.Count == 0))
            {
                return;
            }

            // Извлекаем рёбра из стеков.
            Edge xEdge = xEdges.Pop();
            Edge yEdge = yEdges.Pop();

            edgesList.ReplaceEdge(yEdge, xEdge);
            // Убираем выгоду за обмен из переменной для учёта суммы выгод.
            gainSum -= xEdge.Length - yEdge.Length;

            // Так как мы возвращаем ребро на тур, нужно заменить связь в Edge3 на лежащую на туре связь (если было три связи).
            // Делаем это, только если отменяли второй и более обмен (т.е. в стеках ещё есть элементы).
            if (xEdges.Count == 0)
            {
                return;
            }

            Node node;

            // Выяснем, на каком узле добавлено третье ребро.
            if (xEdge.StartNode.Edge3 == xEdge)
                node = xEdge.StartNode;
            else if (xEdge.EndNode.Edge3 == xEdge)
                node = xEdge.EndNode;
            else
                return;
            // Получаем предыдущее добавленное ребро.
            Edge prevY = yEdges.Peek();
            if (node.Edge1 == prevY)
            {
                node.Edge1 = xEdge;
                node.Edge3 = prevY;
            }
            else //if (node.Edge2 == prevY)
            {
                node.Edge2 = xEdge;
                node.Edge3 = prevY;
            }
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Отменяет указанное количество обменов.
        /// </summary>
        private void UndoSeriesOfReplaces(int k)
        {
            if (k <= 0)
                return;

            if ((xEdges.Count != yEdges.Count) || (xEdges.Count < k))
            {
                return;
            }

            for (int i = 1; i <= k; i++)
            {
                UndoReplace();
            }
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Делает обмен рёбер согласно всем правилам (добавляет их в стеки, записывает выгоду от обмена).
        /// </summary>
        private bool DoSwap(Edge xEdge, Edge yEdge)
        {
            bool res = edgesList.ReplaceEdge(xEdge, yEdge);
            if (res == false)
                return false;
            // Если обмен успешен, добавляем в стеки обмененные рёбра.
            xEdges.Push(xEdge);
            yEdges.Push(yEdge);
            // И учитываем разницу от обмена. 
            gainSum += xEdge.Length - yEdge.Length;

            return true;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Подготавливает список узлов к работе алгоритма ЛК.
        /// </summary>
        private void PrepareNodes()
        {
            // Очищаем теги.
            nodesList.ClearTags();
            // Создаём матрицу стоимости, если не была создана ранее.
            if (nodesList.CostMatrix == null)
                nodesList.CreateCostMatrix();
            // Ассоциируем строки матрицы стоимости с узлами, если не было сделано ранее.
            if (!nodesList.IsCostsAssociates)
                nodesList.AssociateCosts();
            // Если изначально заданы данные в виде матрицы, то заменяем нули на большие числа.
            if (nodesList.Distance == Distances.Distance_EXPLICIT)
            {
                nodesList.SetNullesToBigNumber();
            }

            Logs.Notify("Формируем списки ближайших соседей для каждого узла...", 4);
            // Создаём ближайших соседей у каждого узла.
            nodesList.CreateNeighbors();
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Процедура производит необходимые манипуляции, чтобы сделать тур в работе текущим и дать возможность начать последовательность поиска заново. Возвращает true, если нужно прекратить поиск тура.
        /// </summary>
        private bool ImprovementFound()
        {
            double oldGS = totalGain;
            totalGain += gainSum;
            gainSum = 0;
            edgesList.BuildNodes(nodesList, false);
            nodesList.ClearTags();
            edgesList.ClearTags();
            xEdges.Clear();
            yEdges.Clear();
            Logs.Notify("Выгода: " + totalGain, 3);
            if ((totalGain - oldGS) <= gap)
                return true;
            else
                return false;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Возвращает первый ещё неопробованный узел из переданного списка узлов. Если такого нет, то возвращает null.
        /// Если параметр marking установлен как false, то найденный узел не будет помечен.  
        /// </summary>
        private Node FindFirstUntriedNode(bool marking = true)
        {
            Node n = nodesList.FirstNode;
            do
            {
                if (n.Tag != 1)
                {
                    if (marking)
                        n.Tag = 1;
                    return n;
                }
                n = n.Succ;
            } while (n != nodesList.FirstNode);
            return null;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Возвращает первый ещё неопробованный узел из переданного списка, не лежащий на туре, и чтобы разница от переданной стоимости и длины от этого узла до узла node + сумма предыдущих выгод, была больше нуля. Если такого нет, возвращает null.
        /// Если параметр marking установлен как false, то найденный узел не будет помечен.  
        /// </summary>
        private Node FindFirstUntriedNodeNotInTour(Node node, double cost, int tag, out double gain, bool marking = true)
        {
            Node pred = node.Edge1.OtherNode(node);
            Node succ = node.Edge2.OtherNode(node);
            Node n = null;
            for (int i = 1; i <= nearestNeighbors;  i++)
            {
                n = node.Neighbors.ElementAt(i).Node;

                // Если выбираем t3 (для y1)
                if (tag == -1)
                {
                    if (n.Tag2 == node.Id)
                        continue;
                }

                // Если выбираем t5 (для y2)
                if (tag == -2)
                {
                    if (n.Tag3 == node.Id)
                        continue;
                }

                // Проверяем, можно ли использовать этот узел (не был использован для текущего k).
                if ((n.Tag4 == tag) && (n.Tag5 == node.Id))
                {
                    continue;
                }

                // Узел не должен быть равен самому себе, следующему и предыдущему узлам. 
                if ((n != node) && (n != succ) && (n != pred))
                {
                    // Выгода от обмена.
                    gain = cost - nodesList.Length(n, node);
                    // Сумма выгод должна быть положительной.
                    if (gainSum + gain <= 0.00001)
                    {
                        gain = 0;
                        continue;
                    }

                    // Проверяем, не была ли потенциальная связь ранее в удалённых рёбрах x.
                    if (IsInStack(xEdges, node, n))
                    {
                        gain = 0;
                        continue;
                    }

                    // Если прошли проверки выши, то помечаем узлы как использованные.
                    if (marking)
                    {
                        if ((tag != -1) && (tag != -2))
                        {
                            n.Tag4 = tag; // Номер k-обмена (отрицательный).
                            n.Tag5 = node.Id; // Номер идентификатора, от которой строим связь.
                        }
                        else
                        {
                            // Если первый или второй обмен.
                            if (tag == -1)
                                n.Tag2 = node.Id;
                            if (tag == -2)
                                n.Tag3 = node.Id;
                        }
                        return n;
                    }
                }
            }
            gain = 0;
            return null;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Пытается добавить в список рёбр edgesList ребро Y и удалить ребро X.
        /// Если в результате получается тур, возвращает true, иначе отменяет произведённые действия.
        /// В переменной gain возвращает выгоду от обмена (или 0, если обмен неуспешен).
        /// В переменно posTotalGain возвращает true, если общая выгода (gainSum) была положительна. 
        /// Необязательный параметр negGainBacktracking делает откат обмена в случае отрицательной выгоды. 
        /// </summary>
        private bool TrySwap(Edge xEdge, Edge yEdge, out double gain, out bool posTotalGain, bool negGainBacktracking = false)
        {
            DoSwap(xEdge, yEdge);
            if (edgesList.IsTourClosed())
            {
                gain = xEdge.Length - yEdge.Length;
                // Откатываем обмен, если выгода отрицательна от обмена и задан параметр.
                if (negGainBacktracking)
                {
                    if (gainSum > 0.00001)
                    {
                        posTotalGain = true;
                    }
                    else
                    {
                        UndoReplace();
                        posTotalGain = false;

                    }
                }
                else
                {
                    if (gainSum > 0.00001)
                    {
                        posTotalGain = true;
                    }
                    else
                    {
                        posTotalGain = false;
                    }
                }
                return true;
            }
            else
            {
                // Не удалось закрыть тур, откатывает обмен.
                UndoReplace();
                gain = 0;
                posTotalGain = false;
                return false;
            }
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Возвращает и помечает ещё неопробованное ребро от переданной связи, а также второй узел этого ребра. 
        /// </summary>
        private Edge FindFirstXEdge(Node t1, out Node t2)
        {
            if (t1.Edge1.Tag == 0)
            {
                t2 = t1.Edge1.OtherNode(t1);
                t1.Edge1.Tag = 1;
                return t1.Edge1;
            }
            if (t1.Edge2.Tag == 0)
            {
                t2 = t1.Edge2.OtherNode(t1);
                t1.Edge2.Tag = 1;
                return t1.Edge2;
            }
            t2 = null;
            return null;
        }


        //-------------------------------------------------------------------

        /// <summary>
        /// Ищет и возвращает первое ребро Y для обмена.
        /// К нему применяются особые условия для поиска, в отличие от рёбр для K >= 2.
        /// Возвращает ребро и выгоду от обмена.
        /// </summary>
        private Edge FindFirstYEdge(Node t2, Edge x1, out double gain)
        {
            // Ищем ребро Y1.
            gain = 0;
            // Помечаем узлы для y отрицательным числом.
            Node t3 = FindFirstUntriedNodeNotInTour(t2, x1.Length, -1, out gain);
            if (t3 != null)
            {
                Edge yEdge = new Edge(t2, t3);
                yEdge.Tag = -1;
                return yEdge;
            }
            return null;
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Возвращает и помечает ребро, лежащее на на туре и подходящее для обмена, удовлетворяя следующим условиям:
        /// 1. Если конечный узел ребра соединить с начальным узлом тура (tFirst, узла, 
        /// с которого начались обмены), образовывается тур;
        /// 2. Ребро не равно ни одному из множества ранее обмененных рёбер y.
        /// Возвращает выгоду от обмена и само ребро (0 и null, если не получлось найти подходящее ребро соответственно).
        /// Параметр toTFirstPosGain сообщает, является ли положительной выгода от последующего соединения с t1. 
        /// </summary>
        private Edge FindXEdgeToSwap(Node node, Node tFirst, int curK, out double gain, out bool toTFirstPosGain)
        {
            // Есть два выбора для обмена — следующий и предыдущий узел.
            // Но только один из них даст возможность закрыть тур (соеденив с tFirst, с первым узлом).
            // Пробуем оба варианта.
            // Если обмен даст положительную общую выгоду, то не будем отменять его.
            // Поэтому метод помимо того, что вернёт обменянное ребро, также косвенно изменит список связей (тур). 
            Edge edge1 = node.Edge1;
            Edge edge2 = node.Edge2;
            bool allowE1 = true;
            bool allowE2 = true;

            // Ранее иcпользуемые рёбра y имеют отрицательный тег.
            // Ребро x не должно быть ранее использоваться в качестве ребра y (или в уже опробовано как ребро x на этой итерации).
            if (((edge1.Tag < 0) && (edge1.Tag > (-1 * curK))) || (edge1.Tag == curK))
            {
                 allowE1 = false;
            }
            if (((edge2.Tag < 0) && (edge2.Tag > (-1 * curK))) || (edge2.Tag == curK))
            {
                allowE2 = false;
            }

            if (allowE1 && (node.Edge1.OtherNode(node) != tFirst) && TrySwap(edge1, new Edge(node.Edge1.OtherNode(node), tFirst), out gain, out toTFirstPosGain, true))
            {
                edge1.Tag = curK;
                return edge1;
            }
            else
            {
                // Вариант с другим ребром.
                if (allowE2 && (node.Edge2.OtherNode(node) != tFirst) && TrySwap(edge2, new Edge(node.Edge2.OtherNode(node), tFirst), out gain, out toTFirstPosGain, true))
                {
                    edge2.Tag = curK;
                    return edge2;
                }
                else
                {
                    // Обмены в обе стороны не удались.
                    gain = 0;
                    toTFirstPosGain = false;
                    return null;
                }
            }
        }

        //-------------------------------------------------------------------

        /// <summary>
        /// Возвращает и помечает ребро, не лежащее на туре и подходящее для обмена, удовлетворяя следующим условиям:
        /// 1. Сумма всех обменов (gainSum + выгода от текущего обмена) положительна;
        /// 2. Ребро не равно ни одному из множества ранее обмененных рёбр x.
        /// 3. Существует следующее ребро x.
        /// Возвращает выгоду от обмена и само ребро (0 и null, если не получилось найти подходящее ребро соответственно).
        /// </summary>
        private Edge FindYEdgeToSwap(Node node, Edge xEdge, int curK, out double gain)
        {
            // Ищем подходящий узел для ребра.
            Node endNode = FindFirstUntriedNodeNotInTour(node, xEdge.Length, curK, out gain, true);
            if (endNode != null)
            {
                Edge yEdge = new Edge(node, endNode);
                yEdge.Tag = curK;
                return yEdge;
            }
            else
            {
                gain = 0;
                return null;
            }
        }
 
        //===============================================================================
        // Public

        /// <summary>
        /// Конструктор класса. На входе принимает список узлов с первоначальным туром.
        /// </summary>
        public LinKernighanSolver(NodesList nodesList, int nearestNeighborsCount, double gapValue)
        {
            xEdges = new Stack<Edge>();
            yEdges = new Stack<Edge>();
            this.nodesList = nodesList;
            totalGain = 0;

            // Задаём параметры.
            if (nearestNeighborsCount >= nodesList.Dimension)
                nearestNeighborsCount = nodesList.Dimension - 1;
            if (nearestNeighborsCount <= 0)
                nearestNeighborsCount = 1;
            nearestNeighbors = nearestNeighborsCount;

            if (gapValue < 0)
                gapValue = 0;
            gap = gapValue;
        }

        /// <summary>
        /// Вычисление тура с помощью алгоритма Лина-Кернигана. Возвращает стоимость нового тура.
        /// </summary>
        public double Solve()
        {
            // Выходим, если список узлов не существует.
            if (nodesList == null)
                return 0.0;

            PrepareNodes(); // Подготавливаем узлы к работе.
            edgesList = new EdgesList(nodesList); // Создаём список рёбр на основе переданного списка узлов.
            double bestCost = nodesList.Cost; // Изначальная стоимость тура.
            double gain = 0; // Выгода от текущего одного обмена.
            gainSum = 0; // Сумма выгод от обменов в текущей серии обменов. 
            totalGain = 0; // Общая выгода от обменов. 
            bool toT1PosGain = false; // Флаг, сообщающий, что при соединении узла с t1 будет положительная выгода.

            // Первые узлы для первых обменов.
            Node t1 = null, t2 = null;
            Node t2iPred2KSwap = null, t2iPredOld = null, t2iPred = null;
            // Очищаем списки удалённых и добавленных рёбер.
            xEdges.Clear();
            yEdges.Clear();

            int i = 1; // Номер текущего обмена.
            
            // Рёбра для обменов.
            Edge xEdge = null, x1 = null, x2 = null, xPred = null;
            Edge yEdge = null, y1 = null, y2 = null;

            // Принятые договорённости. 
            // Рёбра x помечаем положительным номером обмена.
            // Рёбра y помечаем отрицательным номером обмена.
            // Первый тег узла помечаем числом 1 при поиске связи для t1. 
            // Второй тег узла помечаем -1 при отмечании опробованной альтернативы для t3.
            // Третий тег узла помечаем -2 при отмечании опробованной альтернативы для t5.
            // Четвёртый тег узла помечаем отрицательным номером k при k > 2.
            // Пятый тег узла помечаем номером узла, от которого к текущему узлу пробовалась связь при k > 2.
            // Например, Tag4 = -3, Tag2 = 5 для узла 2 будет означать, что на третьем обмене пробовалась связь 5 → 2.

            // Пробуем все альтернативы для t1, пока не будет найдена подходящяя пара для обмена.
            Start:
            while ((t1 = FindFirstUntriedNode()) != null)
            {
                i = 1;

                // Пока есть альтернативы для x1.
                while ((x1 = FindFirstXEdge(t1, out t2)) != null)
                {
                    // Пока есть альтернативы для y1
                    while ((y1 = FindFirstYEdge(t2, x1, out gain)) != null)
                    {
                        // Делаем обмен. Тур после первого обмена ещё не закрыт, поэтому для обмена используем не TrySwap, а более низкоуровневый метод DoSwap.
                        DoSwap(x1, y1);

                        // Узнаём крайний узел, от которого будем продолжать обмены.
                        t2iPred2KSwap = y1.OtherNode(t2);
                        // Ищем x2, для неё есть два выбора, но только один даст возможность закрыть тур.
                        x2 = FindXEdgeToSwap(t2iPred2KSwap, t1, 2, out gain, out toT1PosGain);

                        // Если x2 найти не удалось, пробуем другой y1 и отменяем замену x1 на y1.
                        if (x2 == null)
                        {
                            UndoReplace();
                            continue;
                        }

                        // Если x2 нашли.
                        // Если есть выгода (от соединения с t1), то начинаем сначала, но уже с новым туром. 
                        if (toT1PosGain)
                        {
                            if (ImprovementFound())
                                goto End;
                            goto Start;
                        }

                        // Если же выгоды нет, то продолжаем последовательный обмен.
                        t2iPred2KSwap = x2.OtherNode(t2iPred2KSwap);

                        // Пока есть альтернативы для y2.
                        while ((y2 = FindYEdgeToSwap(t2iPred2KSwap, x2, -2, out gain)) != null)
                        {
                        //    Logs.Notify("Меняем x2 на Y2:" + x2.ToString() + " " + y2.ToString(), 0);
                            DoSwap(x2, y2);
                            t2iPred = y2.OtherNode(t2iPred2KSwap);

                            // Далее пробуем серии последовательных обменов до тех пор, пока не закроем тур или не найдём одно из рёбр для обмена (xi или yi).
                            i = 2;
                            while (true)
                            {
                                i++;
                               
                                xEdge = FindXEdgeToSwap(t2iPred, t1, i, out gain, out toT1PosGain);
                                if (xEdge == null)
                                {
                                    // Откатываем последний обмен, если ребро x не существует, так как его существование является одним из критериев поиска ребра y на следующем шаге.
                                    // Делаем это только если уже был поиск y (то есть на шаге i>3).
                                    if (i > 3)
                                    {
                                        UndoReplace();
                                        i--;
                                        t2iPred = t2iPredOld;
                                        xEdge = xPred;
                                    }
                                    else
                                    {
                                        UndoReplace();
                                        i = 2;
                                        break;
                                    }
                                }
                                else
                                {
                                    // Если есть выгода (от соединения с t1), то начинаем с начала, но уже с новым туром. 
                                    if (toT1PosGain)
                                    {
                                        if (ImprovementFound())
                                            goto End;
                                        goto Start;
                                    }
                                    t2iPred = xEdge.OtherNode(t2iPred);
                                }

                                // Иначе пробуем найти y1.  
                                yEdge = FindYEdgeToSwap(t2iPred, xEdge, -i, out gain);

                                // Если такого ребра нет, откатываем все произведённые обмены вплоть до второго шага.

                                if (yEdge == null)
                                {
                                    UndoSeriesOfReplaces(i - 2);
                                    nodesList.ClearTags4and5();
                                    break;
                                }

                                // Если рёбра найдены, то меняем их.
                                DoSwap(xEdge, yEdge);
                                // xPred м t2iPredOld используем на случай, если на следующем шаге не удастся найти новое ребро x. Если так случится, попробуем найти новое ребро y.
                                t2iPredOld = t2iPred;
                                xPred = xEdge;

                                // Следующий узел, с которого продолжаем обмены.
                                t2iPred = yEdge.OtherNode(t2iPred);
                            }
                        }
                        // Если не нашли вариант для y2, то откатываем замену x1 на y1 и пробуем новый вариант для y1.
                        if (y2 == null)
                        {
                            UndoReplace();
                            nodesList.ClearTags3();
                        }
                    }
                    nodesList.ClearTags2();
                }

                // Все варианты для X1 опробованы, теперь очищаем теги с пометками опробованных вариантов для x1, x2, y1, y2 и т.д. Пометки для t1 (в tag1) оставляем.
                nodesList.ClearTags2and3();
                edgesList.ClearTags();
                t1.Edge1.Tag = 0;
                t1.Edge2.Tag = 0;
            }

            End:
            double tourCost = nodesList.Cost;
            if (tourCost < nodesList.BestCost)
                nodesList.BestCost = tourCost;
            return tourCost;
        }

    }
}
