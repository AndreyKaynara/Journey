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

using JourneyExceptions;

/*
 * Абстрактный тип, основывающийся на понятии ребра (в отличии от nodesList). 
 * Тур образовывается последовательностью связанных рёбер. 
 */

namespace JourneyTSP
{

    /// <summary>
    /// Класс Edge представляет ребро (связь) тура.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Начальный узел ребра.
        /// </summary>
        public Node StartNode = null;
        /// <summary>
        /// Конечный узел ребра.
        /// </summary>
        public Node EndNode = null;
        /// <summary>
        /// // Данные, связанные с ребром. 
        /// </summary>
        public int Tag; 
        /// <summary>
        /// Флаг, сообщающий что узел в туре.
        /// </summary>
        public bool InTour;  

        //===============================================================================
        // Свойства класса.

        /// <summary>
        /// Имя ребра, состоящее из идентификаторов первого и второго узлов.
        /// </summary>
        public string Name
        {
            get { return StartNode.Id.ToString() + " → " + EndNode.Id.ToString(); }
        }

        /// <summary>
        /// Возвращает длину ребра.
        /// </summary>
        public double Length
        {
            get
            {
                return StartNode.Costs[EndNode.Id - 1]; // Возвращает расстояние от начального до конечного узла.
            }
        }

        //===============================================================================
        // Методы класса.

        /// <summary>
        /// Конструктор класса. Создаёт ребро с вершинами, переданными в параметрах.
        /// </summary>
        public Edge(Node startNode, Node endNode)
        {

            StartNode = startNode;
            EndNode = endNode;
        }

        /// <summary>
        /// Возвращает следующее ребро. В параметре как необязательный параметр можно передать предыдущее ребро. Если оно не передаётся, то возвращается первое подходящее ребро. 
        /// </summary>
        public Edge Next(Edge prevEdge = null)
        {
            // Выходим, если в переменных переданного ребра есть нулевые переменные. 
            if (prevEdge != null)
            {
                if ((prevEdge.StartNode == null) || (prevEdge.EndNode == null) ||
                    (prevEdge.StartNode.Edge1 == null) || (prevEdge.StartNode.Edge2 == null) ||
                    (prevEdge.EndNode.Edge1 == null) || (prevEdge.EndNode.Edge2 == null))
                {
                    return null;
                }
            }
            // Ищем подходящий вариант. 
            // Всего у нас четыер варианта ребра. Два в рёбрах, ассоциируемых с одним узлом, два — с другим.
            // Два из ассоциируемых рёбр равны самому текущему ребру.

            // Смотрим по конечному узлу.
            // Проверяем первое ребро.

            if (this.EndNode.Edge1 != this)
            {
                if (prevEdge != null)
                {
                    if (this.EndNode.Edge1 != prevEdge)
                        return this.EndNode.Edge1; // Ребро подходит, если оно не предыдущее и не текущее. 
                }
                else
                {
                    return this.EndNode.Edge1;
                }
            }
            // Проверяем второе ребро.
            if (this.EndNode.Edge2 != this)
            {
                if (prevEdge != null)
                {
                    if (this.EndNode.Edge2 != prevEdge)
                        return this.EndNode.Edge2;
                }
                else
                {
                    return this.EndNode.Edge2;
                }

            }
            // Смотрим по начальному узлу, если ещё не нашли подходящего ребра.
            // Проверяем первое ребро.
            if (this.StartNode.Edge1 != this)
            {
                if (prevEdge != null)
                {
                    if (this.StartNode.Edge1 != prevEdge)
                        return this.StartNode.Edge1;
                }
                else
                {
                    return this.StartNode.Edge1;
                }
            }
            // Проверяем второе ребро.
            if (this.StartNode.Edge2 != this)
            {
                if (prevEdge != null)
                {
                    if (this.StartNode.Edge2 != prevEdge)
                        return this.StartNode.Edge2;
                }
                else
                {
                    return this.StartNode.Edge2;
                }
            }
            return null; // Ничего не возвращаем, если не нашли подходящее ребро. 
        }

        /// <summary>
        /// Возвращает узел ребра, отличный от переданного (то есть другой узел этого ребра). 
        /// </summary>
        public Node OtherNode(Node node)
        {
            if (node == StartNode)
            {
                return EndNode;
            }
            else
            {
                return StartNode;
            }
        }

        /// <summary>
        /// Проверяет, является ли связь равной переданной. Связи считаются эквивалентными, если имеют одинаковыми узлы (в любом порядке). 
        /// </summary>
        public bool IsEqual(Edge edge)
        {
            // Проверяем оба варианта направленности.
            if (StartNode == edge.StartNode)
            {
                if (EndNode == edge.EndNode)
                {
                    return true;
                }
            }
            else if (StartNode == edge.EndNode)
            {
                if (EndNode == edge.StartNode)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет, соотвествуют ли переданные узлы узлам в ребре. Порядок передачи параметров не важен, определяется лишь наличие узлов с указанными идентификаторами в ребре. 
        /// </summary>
        public bool IsEqual(Node A, Node B)
        {
            if (StartNode == A)
            {
                if (EndNode == B)
                    return true;
            }
            else if (EndNode == B)
            {
                if (StartNode == A)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает ребро в строковом виде (начальный и конечный узлы).
        /// </summary>
        override public string ToString()
        {
            return StartNode.Id.ToString() + "→" + EndNode.Id.ToString();
        }

    }

    /// <summary>
    /// Список рёбр (связей) тура.
    /// </summary>
    public class EdgesList
    {
        // Поля класса. 
        private List<Edge> edgesList = new List<Edge>();

        //===============================================================================
        // Свойства класса.

        /// <summary>
        /// Количество рёбер в туре.
        /// </summary>
        public double Count
        {
            get { return edgesList.Count; }
        }

        //===============================================================================
        // Методы класса. 

        // Private

        /// <summary>
        /// Проверяет, есть ли переданная связь в туре и если да, то возвращает её индекс.
        /// </summary>
        private bool IsInTour(Edge edge, out int index)
        {
            index = -1;
            // Если в туре нет связей, то выходим. 
            if (edgesList.Count == 0)
            {
                return false;
            }

            // Проходим по всем связям. 
            for (int i = 0; i < edgesList.Count; i++)
            {

                if (edgesList[i].IsEqual(edge))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Возвращает общий узел у этих двух рёбр, а также два оставшихся — отличающихся.  
        /// </summary>
        private void GetCommonAndDifferentNodes(Edge edgeA, Edge edgeB, out Node commonNode, out Node diffNodeA, out Node diffNodeB)
        {
            commonNode = null;
            diffNodeA = null;
            diffNodeB = null;
            if (edgeA.StartNode == edgeB.StartNode)
            {
                commonNode = edgeA.StartNode;
                diffNodeA = edgeA.EndNode;
                diffNodeB = edgeB.EndNode;

            }
            else if (edgeA.StartNode == edgeB.EndNode)
            {
                commonNode = edgeA.StartNode;
                diffNodeA = edgeA.EndNode;
                diffNodeB = edgeB.StartNode;
            }
            else if (edgeA.EndNode == edgeB.StartNode)
            {
                commonNode = edgeA.EndNode;
                diffNodeA = edgeA.StartNode;
                diffNodeB = edgeB.EndNode;
            }
            else if (edgeA.EndNode == edgeB.EndNode)
            {
                commonNode = edgeA.EndNode;
                diffNodeA = edgeA.StartNode;
                diffNodeB = edgeB.StartNode;
            }
        }


        //==============================================================================
        // Public

        /// <summary>
        /// Конструктор класса. Формирует сисок рёбр на основе переданного списка узлов.
        /// </summary>
        public EdgesList(NodesList nodes)
        {
            BuildEdges(nodes);
        }

        /// <summary>
        /// Формирует список рёбр согласно переданному туру. Количество рёбр будет равно количеству узлов. 
        /// </summary>
        public void BuildEdges(NodesList nodes)
        {
            edgesList.Clear();
            Node n = nodes.FirstNode;
            int i = 0;
            do
            {
                Edge edge = new Edge(n, n.Succ); // Связь от текущего узла к следующему.
                edge.InTour = true;
                edgesList.Add(edge); // Добавляем связь в список. 
                n.Edge1 = edge; // Связываем связь с узлом. 
                // Для первого узла добавим связь отдельно не в цикле (так как она последняя).
                if (i > 0)
                {
                    n.Edge2 = edgesList[i - 1];
                }
                n = n.Succ;
                i++;
            } while (n != nodes.FirstNode);
            // Добавляем недостающую связь к последгюму узлу для первого узла.
            nodes.FirstNode.Edge2 = edgesList[edgesList.Count - 1];
        }

        /// <summary>
        /// Формирует список узлов согласно имеющемуся списку связей. needCheck задаёт необходимость проверки на корректность
        /// тура перед началом работы над формированием списка.
        /// </summary>
        public bool BuildNodes(NodesList nodesList, bool needCheck = true)
        {
            // Выходим, если тур некорректен.
            if (needCheck)
                if (!IsTourClosed())
                {
                    return false;
                }

            Node prevNode = null; // Предыдущий узел.
            Node node = edgesList[0].StartNode; // Текущий узел.
            Node otherNode = edgesList[0].EndNode; // Другой узел у ребра.
            Edge prevEdge = null; // Предыдущее ребро. 
            Edge edge = edgesList[0]; // Текущее ребро. 
            Edge tempEdge = null; // Временная перемення для хранения ссылки на предыдущее рекро.

            // Пытаемся пройти по туру. 
            do
            {
                // Переходим к следующей связи.
                tempEdge = edge;
                edge = edge.Next(prevEdge);
                prevEdge = tempEdge;

                // Определяем следующий узел (общий у предыдущего и текущего ребра).
                prevNode = node;
                if (edge.StartNode == otherNode)
                {
                    node = edge.StartNode;
                    otherNode = edge.EndNode;
                }
                else
                {
                    node = edge.EndNode;
                    otherNode = edge.StartNode;
                }
                prevNode.Succ = node; // Ссылка на следующий узел у предыдущего.
                node.Pred = prevNode; // Ссылка на предыдущий узел у текущего узла.
            } while (edge != edgesList[0]); // Выходим, когда снова возвратимся к исходной связи.

            return true;
        }

        /// <summary>
        /// Проверяет закрытость тура. Возвращает true, если по рёбрам можно обойти весь тур (то есть тур полностью закрыт: количество обойдённых узлов соотвествует общему количеству узлов). 
        /// </summary>
        public bool IsTourClosed()
        {
            // Выходим и возвращаем false, если у нас нет связей.
            if (edgesList.Count == 0)
                return false;

            int nodesCount = edgesList.Count(); // Количество узлов, которые должны быть в полном туре.

            Edge edge = edgesList[0]; // Начинаем с первой связи.
            int i = 0; // Счётчик пройденных узлов. Если тур цельный, то значение должно быть равно nodesCount.

            Edge prevEdge = null; // Предыдущее ребро. 
            Edge tempEdge = null; // Временная перемення для хранения ссылки на предыдущее ребро.

            // Пытаемся пройти по туру. 
            do
            {
                i++; // Считаем пройденные узлы.

                // Защита от неверных связей. 
                // Выходим, если уже было больше проходов, чем всего узлов.
                // То есть мы так и не вернулись в начальное ребро, но проходов уже было больше. 
                if (i > nodesCount)
                    return false;

                if (edge == null)
                {
                    break;
                }

                if (!edge.InTour)
                {
                    break;
                }

                // Переходим к следующей связи.
                tempEdge = edge;
                edge = edge.Next(prevEdge);
                prevEdge = tempEdge;

                // Если произошла ошибка с установлением следующего узла, выходим из цикла. 
                if (edge == null)
                    break;
            } while (edge != edgesList[0]); // Выходим, когда снова возвратимся к исходной связи.

            if (i == nodesCount)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Возвращает ребро по переданной позиции в списке.
        /// </summary>
        public Edge ElementAtPos(int pos)
        {
            if ((pos <= 0) || (pos > edgesList.Count))
                throw new EdgesListException("Некорректная переданная позиция ребра в EdgesList.");

            int nodesCount = edgesList.Count(); // Количество узлов, которые должны быть в полном туре.

            Edge edge = edgesList[0]; // Начинаем с первой связи.
            int i = 1; // Счётчик пройденных узлов. Если тур цельный, то значение должно быть равно nodesCount.

            Edge prevEdge = null; // Предыдущее ребро. 
            Edge tempEdge = null; // Временная перемення для хранения ссылки на предыдущее ребро.

            if (pos == 1)
                return edge;

            // Пытаемся пройти по туру. 
            do
            {
                i++; // Считаем пройденные узлы.

                // Защита от неверных связей. 
                // Выходим, если уже было больше проходов, чем всего узлов.
                // То есть мы так и не вернулись в начальное ребро, но проходов уже было больше. 
                if (i > nodesCount)
                    return null;

                if (edge == null)
                {
                    break;
                }

                if (!edge.InTour)
                {
                    break;
                }

                // Переходим к следующей связи.
                tempEdge = edge;
                edge = edge.Next(prevEdge);
                prevEdge = tempEdge;

                if (i == pos)
                    return edge;

                // Если произошла ошибка с установлением следующего узла, выходим из цикла. 
                if (edge == null)
                    break;
            } while (edge != edgesList[0]); // Выходим, когда снова возвратимся к исходной связи.
            return null;
        }

        /// <summary>
        /// Заменяет ребро A на ребро B в списке связей и связывает оборванные узлы с заменяемым ребром, а где необходимо — удаляет связи. 
        /// </summary>
        public bool ReplaceEdge(Edge edgeA, Edge edgeB)
        {
            int indexA;
            // Определяем индекс заменяемого ребра.
            indexA = edgesList.IndexOf(edgeA);
            if (indexA == -1)
                return false;
 
            // Получаем общий и отличающийся узлы у рёбер.
            Node commonNode, diffA, diffB;
            GetCommonAndDifferentNodes(edgeA, edgeB, out commonNode, out diffA, out diffB);
            if ((commonNode == null) || (diffA == null) || (diffB == null))
                return false;
            // Меняем общему узлу с заменяемым ребром узлу связь (ту, которую удаляет, на ту, которую заменяем). 
            if (commonNode.Edge1 == edgeA)
            {
                commonNode.Edge1 = edgeB;
            } else if (commonNode.Edge2 == edgeA)
            {
                commonNode.Edge2 = edgeB;
            }
            else
            {
                return false;
            }

            // Указываем, что первое ребро уже не в туре, а второе в туре.
            edgeA.InTour = false;
            edgeB.InTour = true;

            // Если у узла нет удаленных рёбр, то записываем добавляемую переменную в третий слот Edge (то есть с ребром будет ассоциировано три ребра).
            if ((diffB.Edge1 != null) && (diffB.Edge2 != null))
              diffB.Edge3 = edgeB;

            // Теперь убираем связь у «оборванного» узла.
            // Меняем связь на Edge3. При этом если ранее она была задана, 
            // то значит в этом случае мы записываем третье ребро вместо оборванного ранее.
            // Иначе связь оборвётся (так как Edge3 может быть = null).
            if (diffA.Edge1 == edgeA)
            {
                diffA.Edge1 = diffA.Edge3; 
                diffA.Edge3 = null;
            }
            else if (diffA.Edge2 == edgeA)
            {
                diffA.Edge2 = diffA.Edge3;
                diffA.Edge3 = null;
            }
            else
            {
                diffA.Edge3 = null;
            }

            // Меняем связь на переданную. 
            edgesList[indexA] = edgeB;     
            // Смотрим, есть ли в узлах заменённой связи «пустые» переменные с ссылками на связи.
            // Если есть, то ставим ссылку на добавленную связь.     
            if (edgesList[indexA].StartNode.Edge1 == null)
            {
                edgesList[indexA].StartNode.Edge1 = edgesList[indexA];
            }
            if (edgesList[indexA].StartNode.Edge2 == null)
            {
                edgesList[indexA].StartNode.Edge2 = edgesList[indexA];
            }
            if (edgesList[indexA].EndNode.Edge1 == null)
            {
                edgesList[indexA].EndNode.Edge1 = edgesList[indexA];
            }
            if (edgesList[indexA].EndNode.Edge2 == null)
            {
                edgesList[indexA].EndNode.Edge2 = edgesList[indexA];
            }

            return true;
        }

        /// <summary>
        /// Устанавливает в ноль все теги рёбр.
        /// </summary>
        public void ClearTags()
        {
            for (int i = 0; i < edgesList.Count(); i++)
            {
                edgesList[i].Tag = 0;
            }
        }

        /// <summary>
        /// Возвращает строку со снимком текущего состояния списка рёбр.
        /// </summary>
        public string CreateEdgesDump()
        {
            StringBuilder str = new StringBuilder();
            str.Append("==============================" + Environment.NewLine);
            str.Append("Снимок состояния списка рёбр EdgesList" + Environment.NewLine);
            str.Append("Время: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + Environment.NewLine);
            str.Append("Количество рёбр: " + edgesList.Count.ToString() + Environment.NewLine);
            for (int i = 0; i < edgesList.Count; i++)
            {
                str.Append("------------------------------" + Environment.NewLine);
                str.Append("Ребро #" + i.ToString() + Environment.NewLine);
                str.Append("Имя: " + edgesList[i].Name + Environment.NewLine);
                str.Append("Длина: " + edgesList[i].Length + Environment.NewLine);
                str.Append("Тег: " + edgesList[i].Tag + Environment.NewLine);
                str.Append("StartNode:" + Environment.NewLine);
                if (edgesList[i].StartNode != null)
                {
                    if (edgesList[i].StartNode.Edge1 != null)
                        str.Append("— Edge1 = " + edgesList[i].StartNode.Edge1.Name + Environment.NewLine);
                    else
                        str.Append("— Edge1 = null" + Environment.NewLine);

                    if (edgesList[i].StartNode.Edge2 != null)
                        str.Append("— Edge2 = " + edgesList[i].StartNode.Edge2.Name + Environment.NewLine);
                    else
                        str.Append("— Edge2 = null" + Environment.NewLine);

                    if (edgesList[i].StartNode.Edge3 != null)
                        str.Append("— Edge3 = " + edgesList[i].StartNode.Edge3.Name + Environment.NewLine);
                    else
                        str.Append("— Edge3 = null" + Environment.NewLine);
                }
                else
                {
                    str.Append("StartNode = null");
                }
                str.Append("EndNode:" + Environment.NewLine);
                if (edgesList[i].EndNode != null)
                {
                    if (edgesList[i].EndNode.Edge1 != null)
                        str.Append("— Edge1 = " + edgesList[i].EndNode.Edge1.Name + Environment.NewLine);
                    else
                        str.Append("— Edge1 = null" + Environment.NewLine);

                    if (edgesList[i].EndNode.Edge2 != null)
                        str.Append("— Edge2 = " + edgesList[i].EndNode.Edge2.Name + Environment.NewLine);
                    else
                        str.Append("— Edge2 = null" + Environment.NewLine);

                    if (edgesList[i].EndNode.Edge3 != null)
                        str.Append("— Edge3 = " + edgesList[i].EndNode.Edge3.Name + Environment.NewLine);
                    else
                        str.Append("— Edge3 = null" + Environment.NewLine);
                }
                else
                {
                    str.Append("EndNode = null");
                }
            }
            str.Append("==============================" + Environment.NewLine);
            return str.ToString();
        }

        /// <summary>
        /// Возвращает список связей в виде строки.
        /// </summary>
        override public string ToString()
        {
            if (edgesList.Count == 0)
                return "";
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < edgesList.Count; i++)
            {
                str.Append(edgesList[i].ToString());
                if (i != edgesList.Count - 1)
                    str.Append(Environment.NewLine);
            }
            return str.ToString();
        }

    }

}