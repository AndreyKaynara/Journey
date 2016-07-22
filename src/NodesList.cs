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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

using JourneyExceptions;

/*
 * В модуле описываются основные типы и структуры данных, служащие основой для представления данных задачи. 
 */

namespace JourneyTSP
{
    /// <summary>
    /// Структура для хранения координат узла.
    /// </summary>
    public struct Coordinates
    {
        public double X, Y, Z;

        public void Coords(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    /// <summary>
    /// Узлы для хранения в очереди с приоритетом. 
    /// </summary>
    public class NodeCost: FastPriorityQueueNode
    {
        public Node Node;
        public NodeCost(Node node)
        {
            Node = node;
        }
    }

    /// <summary>
    /// Класс Node используется для представление узлов (городов) задачи.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Идентификатор узла в дипазоне [1 .. DIMENSION]
        /// </summary>
        public int Id;
        /// <summary>
        /// Ссылка на предыдущий узел.
        /// </summary>
        public Node Pred = null;
        /// <summary>
        /// Ссылка на следующий узел.
        /// </summary>
        public Node Succ = null; 
        /// <summary>
        /// Ссылка к первой ассоциируемой с узлом связи. 
        /// </summary>
        public Edge Edge1;
        /// <summary>
        /// Ссылка ко второй ассоциируемой с узлом связи. 
        /// </summary>
        public Edge Edge2;
        /// <summary>
        /// Ссылка к третей ассоциируемой с узлом связи. 
        /// </summary>
        public Edge Edge3; // Переменная для хранения временной связи. 
        /// <summary>
        /// Координаты узла.
        /// </summary>
        public Coordinates Coords; 
        /// <summary>
        /// Очередь с расстояниями до ближайших узлов от этого узла. 
        /// </summary>
        public FastPriorityQueue<NodeCost> Neighbors;
        /// <summary>
        /// Стоимость маршрутов к различным узлам от этого узла. 
        /// </summary>
        public double[] Costs = null;
        /// <summary>
        /// Переменная-тег для отмечания узлов. 
        /// </summary>
        public int Tag = 0;
        /// <summary>
        /// Вторая переменная-тег для отмечания узлов.
        /// </summary>
        public int Tag2 = 0;  
        /// <summary>
        /// Третья переменная-тег для отмечания узлов.
        /// </summary>
        public int Tag3 = 0; 
        /// <summary>
        /// Четвёртая переменная-тег для отмечания узлов.
        /// </summary>
        public int Tag4 = 0; 
        /// <summary>
        /// Пятая переменная-тег для отмечания узлов.
        /// </summary>
        public int Tag5 = 0;  
    
        /// <summary>
        /// Устанавливает в ноль все теги узла.
        /// </summary>
        public void ClearTags()
        {
            Tag = 0;
            Tag2 = 0;
            Tag3 = 0;
            Tag4 = 0;
            Tag5 = 0;
        }

        /// <summary>
        /// Очищаем все поля узла, кроме номера.
        /// </summary>
        public void ClearNode()
        {
            ClearTags();
            Pred = null;
            Succ = null;
            Edge1 = null;
            Edge2 = null;
            Edge3 = null;
            Coords.X = 0;
            Coords.Y = 0;
            Coords.Z = 0;
            if (Neighbors != null)
              Neighbors.Clear();
            Costs = null;
        }

        /// <summary>
        /// Возвращает идентификатор узла в строковом виде. 
        /// </summary>
        public override string ToString()
        {
            return Id.ToString();
        }
    }

    /// <summary>
    ///  Класс NodesList — абстрактный тип данных, представлющий собой список узлов (тур) и операции над ними.
    /// </summary>
    public class NodesList
    {
        public const double BigNumber = 999999999999;

        // Поля класса.
        private List<Node> nodesList = new List<Node>(); // Непосредственно сам список узлов.
        private Node firstNode = null; // Ссылка на первый узел. 
        private Node lastNode = null; // Ссылка на последний узел.
        private double bestCost = double.MaxValue; // Лучшая стоимость тура, устанавливается вручную при вычислениях.   
        private Distance distance = Distances.Distance_1; // Используемая функция при вычислениях расстояний.
        private double[][] costMatrix = null; // Матрица расстояний. 
        private bool isCostsAssociates = false; // Сообщает, связаны ли строки матрицы расстояний с узлами в списке. 
       
        //===============================================================================
        // Свойства класса.

        /// <summary>
        /// Первый узел в туре.
        /// </summary>
        public Node FirstNode
        {
            get { return firstNode; }
            set { 
                    firstNode = value;
                    lastNode = firstNode.Pred;
                }
        }

        /// <summary>
        /// Последний узел в туре. 
        /// </summary>
        public Node LastNode
        {
            get { return lastNode; }
            set { 
                    lastNode = value;
                    firstNode = lastNode.Succ;
                }
        }

        /// <summary>
        /// Считает и возвращает стоимость тура.
        /// </summary>
        public double Cost
        {
            get 
            { 
                return FindTourCost(); 
            }
        }

        /// <summary>
        /// Лучшая найденная стоимость. 
        /// </summary>
        public double BestCost
        {
            get { return bestCost; }
            set { bestCost = value; }
        }

        /// <summary>
        /// Функция для вычислений расстояний.
        /// </summary>
        public Distance Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// Матрица стоимости.
        /// </summary>
        public double[][] CostMatrix
        {
            get { return costMatrix; }
            set 
            { 
                costMatrix = value;
                AssociateCosts();
            }
        }

        /// <summary>
        /// Количество узлов в туре.
        /// </summary>
        public int Dimension
        {
            get { return this.nodesList.Count(); }
        }

        /// <summary>
        /// Сообщает, были ли связаны строки матрицы расстояний с узлами в туре. 
        /// </summary>
        public bool IsCostsAssociates
        {
            set { isCostsAssociates = value; }
            get { return isCostsAssociates; }
        }

        //===============================================================================
        // Методы класса.

        // Private

        /// <summary>
        /// Считает текущую стоимость тура, используя для этого координаты (если есть) или матрицу расстояний.
        /// </summary>
        private double FindTourCost()
        {
            Node n = firstNode;
            double result = 0;
            // Если тип расстояний — прямое указание в виде матрицы...
            if (distance == Distances.Distance_EXPLICIT)
            {
                if (costMatrix == null)
                {
                    throw new Exception("Матрица расстояний не создана!");
                }
                // Считаем по матрице расстояний.
                do
                {
                    result = result + n.Costs[n.Succ.Id - 1];
                    n = n.Succ;
                } while (n != FirstNode);
            }
            else
            {
                // Иначе считаем по координатам.
                do
                {
                    result = result + distance(n, n.Succ);
                    n = n.Succ;
                } while (n != FirstNode);
            }
            return Math.Round(result, 2);
        }

        //==============================================================================
        // Public 

        //------------------------------------------------------------------------------
        // Создание и удаление узлов.

        /// <summary>
        /// Создаёт узлы в списке количеством элементов равным Dimension. Старый список очищается. Также удаляется матрица стоимости и другие производные данные. 
        /// </summary>
        public void CreateNodes(int dimension)
        {
            bestCost = double.MaxValue;

            int i;

            if (dimension <= 0)
                throw new NodesListException("DIMENSION меньше или равен нулю.");

            ClearNodes();

            for (i = 0; i < dimension; i++)
            {
                Node node = new Node();

                node.Id = i + 1;

                nodesList.Add(node);

                if ((i != 0) && (i + 1 <= dimension))
                {
                    Link(nodesList[i - 1], nodesList[i]);
                }
            }

            // Первый и последний узлы ссылаются друг на друга.
            nodesList[0].Pred = nodesList[dimension - 1];
            nodesList[dimension - 1].Succ = nodesList[0];

            // Записываем в объект ссылки на первый и последний узлы.
            firstNode = nodesList[0];
            lastNode = nodesList[dimension - 1];
        }

        /// <summary>
        /// Очищает список узлов.
        /// </summary>
        public void ClearNodes()
        {
            if (costMatrix != null)
            {
                for (int i = 0; i < costMatrix.Count(); i++)
                {
                    costMatrix[i] = null;
                }
                costMatrix = null;
            }
            for (int i = 0; i < nodesList.Count; i++)
                nodesList[i].ClearNode();
            nodesList.Clear();
            isCostsAssociates = false;
        }

        //------------------------------------------------------------------------------
        // Инициализация.

        /// <summary>
        /// Процедура связывает затраты из матрицы затрат с каждым узлом (первому узлу — стоимость поездок в первый город, и т.д.).
        /// </summary>
        public void AssociateCosts()
        {
            int i;
            for (i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].Costs = CostMatrix[i];
            }
            isCostsAssociates = true;
        }

        /// <summary>
        /// Создаёт матрицу расстояний, используя введённые координаты.
        /// </summary>
        public void CreateCostMatrix()
        {
            double[][] costMatrix = new double[nodesList.Count][];
            // Сначала просто выделяем память под матрицу.
            for (int i = 0; i < nodesList.Count; i++)
            {
                costMatrix[i] = new double[nodesList.Count];
                costMatrix[i][i] = 0;
            }

            // Теперь считаем расстояния и заполняем матрицу. 
            for (int i = 0; i < nodesList.Count - 1; i++)
            {
                for (int j = i + 1; j < nodesList.Count; j++)
                {
                    costMatrix[i][j] = Distance(nodesList[i], nodesList[j]);
                    costMatrix[j][i] = costMatrix[i][j]; // Зеркально дублируем данные.
                }
            }

            this.costMatrix = costMatrix;
        }

        /// <summary>
        /// Формирует очереди с приоритетом с ближайшими соседями для всех узлов.
        /// </summary>
        public void CreateNeighbors()
        {
            // Создаём матрицу стоимости, если не была создана ранее.
            if (CostMatrix == null)
                CreateCostMatrix();
            // Ассоциируем строки матрицы стоимости с узлами, если не было сделано ранее.
            if (!isCostsAssociates)
                AssociateCosts();

            for (int i = 0; i < nodesList.Count(); i++)
            {
                FastPriorityQueue<NodeCost> priorityQueue = new FastPriorityQueue<NodeCost>(Dimension);
                for (int j = 0; j < nodesList[i].Costs.Count(); j++)
                {
                    NodeCost node = new NodeCost(nodesList[j]);
                    priorityQueue.Enqueue(node, nodesList[i].Costs[j]);
                }
                nodesList[i].Neighbors = priorityQueue;
            }
        }

        //------------------------------------------------------------------------------
        // Доступ к элементам тура.

        /// <summary>
        /// Возвращает элемент по указанному индексу. 
        /// </summary>
        public Node ElementAt(int index)
        {
            return nodesList[index];
        }

        /// <summary>
        /// Возвращает элемент по его местоположению в туре (отсчёт с 1).
        /// </summary>
        public Node ElementAtTourPos(int pos)
        {
            if ((pos < 1) || (pos > Dimension))
                throw new NodesListException("Некорректный индекс позиции узла в туре.");
            if (pos == 1)
                return firstNode;
            if (pos == Dimension)
                return lastNode;

            Node n = firstNode.Succ;
            int i = 2;
            do
            {
                if (pos == i)
                    return n;
                n = n.Succ;
                i++;
            } while (n != FirstNode.Succ);

            return null;
        }

        /// <summary>
        /// Возвращает элемент по его идентификатору.
        /// </summary>
        public Node ElementWithId(int id)
        {
            for (int i = 0; i < nodesList.Count(); i++)
            {
                if (nodesList[i].Id == id)
                    return nodesList[i];
            }
            return null;
        }

        //------------------------------------------------------------------------------
        // Операции с элементами тура.

        /// <summary>
        /// Связывает два узла ссылками друг на друга.
        /// </summary>
        static public void Link(Node a, Node b)
        {
            a.Succ = b;
            b.Pred = a;
        }

        /// <summary>
        /// Связывает два узла так, чтобы b следовал после a.
        /// </summary>
        static public void Follow(Node b, Node a)
        {
            if (a.Succ != b) 
            {
                Link(b.Pred, b.Succ);
                Link(b, a.Succ);
                Link(a, b);
            }       
        }

        /// <summary>
        /// Меняем местами два узла.
        /// </summary>
        static public void Swap(Node a, Node b)
        {
            if (a == b)
                return;
            if (a.Succ == b)
            {
                Follow(b, a);
                return;
            }
            if (a.Pred == b)
            {
                Follow(a, b);
                return;
            }
            a.Succ.Pred = b;
            a.Pred.Succ = b;
            b.Succ.Pred = a;
            b.Pred.Succ = a;
            Node aS = a.Succ;
            Node aP = a.Pred;
            a.Succ = b.Succ;
            a.Pred = b.Pred;
            b.Succ = aS;
            b.Pred = aP;
        }

        /// <summary>
        /// Меняет на обратный порядок следования узлов в туре начиная с узла b, который становится первым и до узла a, который становится последним.
        /// </summary>
        public void Reverse(Node a, Node b)
        {
            // Выходим, если переданы одинаковые узлы. 
            if ((a == b))
            {
                return; 
            }

            if (b.Succ == a)
            {
                Follow(b, a);
                return;
            }

            // Особый случай для двух элементов.
            if (a.Succ == b)
            {
                Follow(a, b);
                return;
            }

            Node n = b; // Начинаем с конечного элемента, который должен стать первым.
            Node next = b.Succ; // Очерёдный элемент, который требуется поставить на первое место.
            Node prevN = a.Pred; // Предыдущий элемент, перед которым должен стать очерёдный.

            while (n != a)
            {
                // Ставим n перед prevN. 
                Follow(n, prevN);
                // Теперь n должеть стать элементом перед новым n.
                prevN = n;
                // Очерёдный элемент, это первый элемент с конца. 
                n = next.Pred;
            }
        }

        //------------------------------------------------------------------------------
        // Очистка тегов.

        /// <summary>
        /// Устанавливает в ноль теги всех узлов. 
        /// </summary>
        public void ClearTags()
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].ClearTags();
            }
        }

        /// <summary>
        /// Устанавливает в ноль второй тег всех узлов.
        /// </summary>
        public void ClearTags2()
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].Tag2 = 0;
            }
        }

        /// <summary>
        /// Устанавливает в ноль третий тег всех узлов.
        /// </summary>
        public void ClearTags3()
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].Tag3 = 0;
            }
        }

        /// <summary>
        /// Устанавливает в ноль и второй и третий теги всех узлов.
        /// </summary>
        public void ClearTags2and3()
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].Tag2 = 0;
                nodesList[i].Tag3 = 0;
            }
        }

        /// <summary>
        /// Устанавливает в ноль и четвёртый и пятый теги всех узлов.
        /// </summary>
        public void ClearTags4and5()
        {
            for (int i = 0; i < nodesList.Count; i++)
            {
                nodesList[i].Tag4 = 0;
                nodesList[i].Tag5 = 0;
            }
        }

        //------------------------------------------------------------------------------
        // Расчёт расстояния.

        /// <summary>
        /// Возвращает расстояние от узла "a" до узла "b" на основе данных из матрицы стоимости.
        /// </summary>
        public double Length(Node a, Node b)
        {
            return Math.Round(a.Costs[b.Id - 1], 2);
        }

        //------------------------------------------------------------------------------
        // Преобразования в строковый вид.

        /// <summary>
        /// Возвращает строку со всеми узлами (идентификатором и координатами) в ней в том виде, в каком они хранятся в структуре данных. 
        /// </summary>
        public string ToString(bool writeTag = false)
        {
            Node n = firstNode;
            StringBuilder str = new StringBuilder();
            do
            {
                str.Append("ID = " + n.Id + System.Environment.NewLine + "X = " + n.Coords.X + " Y = " + n.Coords.Y + " Z = " +     n.Coords.Z + System.Environment.NewLine);
                if (writeTag)
                    str.Append("Tag: " + n.Tag.ToString() + System.Environment.NewLine);
                n = n.Succ;
            } while (n != FirstNode);
            return str.ToString();
        }

        /// <summary>
        /// Возвращает строку с текущим туром. 
        /// </summary>
        public string TourToString()
        {
            Node n = firstNode;
            int i = 1;
            StringBuilder str = new StringBuilder();
            do
            {
                if (i != nodesList.Count())
                    str.Append(n.Id + " → ");
                else
                    str.Append(n.Id.ToString());
                i++;
                n = n.Succ;
            } while (n != FirstNode);
            return str.ToString();
        }

        /// <summary>
        /// Возвращает матрицу расстояний в строковом виде.
        /// </summary>
        public string CostMatrixToString()
        {            
            // Создаём матрицу стоимости, если она не была создана ранее.
            if (costMatrix == null)
                CreateCostMatrix();

            StringBuilder strCostMatrix = new StringBuilder();
        
            for (int i = 0; i < nodesList.Count; i++)
            {
                for (int j = 0; j < nodesList.Count; j++)
                {
                    strCostMatrix.Append(Math.Round(costMatrix[i][j]).ToString());
                    strCostMatrix.Append(" ");
                }
                strCostMatrix.Append(System.Environment.NewLine);
            }
            return strCostMatrix.ToString();
        }

        /// <summary>
        /// Формирует снимок состояние списка узлов в том виде, в каком они хранятся в АТД. 
        /// </summary>
        public string CreateNodesDump()
        {
            StringBuilder str = new StringBuilder();
            str.Append("==============================" + Environment.NewLine);
            str.Append("Снимок состояния списка узлов NodesList" + Environment.NewLine);
            str.Append("Время: " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + Environment.NewLine);
            str.Append("Количество узлов: " + nodesList.Count.ToString() + Environment.NewLine);
            for (int i = 0; i < nodesList.Count(); i++)
            {
                str.Append("------------------------------" + Environment.NewLine);
                str.AppendLine("Узел " + nodesList[i].Id);
                if (nodesList[i].Succ != null)
                    str.AppendLine("Succ:" + nodesList[i].Succ);
                else
                    str.AppendLine("Succ: null");
                if (nodesList[i].Pred != null)
                    str.AppendLine("Pred:" + nodesList[i].Pred);
                else
                    str.AppendLine("Pred: null");
                str.AppendLine("Tag = " + nodesList[i].Tag);
            }
            return str.ToString();
        }


        //------------------------------------------------------------------------------
        // Сохрание и создание тура из массива.

        /// <summary>
        /// Создаёт тур на основе массива целых чисел, каждый из которых — идентификатор. 
        /// </summary>
        public void CreateTourFromArray(int[] tour)
        {
            Node n, oldN, fNode;
            n = ElementWithId(tour[0]);
            fNode = n;
            for (int i = 1; i < tour.Count(); i++)
            {
                oldN = n;
                n = ElementWithId(tour[i]);
                oldN.Succ = n;
                n.Pred = oldN;
            }
            fNode.Pred = n;
            n.Succ = fNode;
            firstNode = fNode;
        }

        /// <summary>
        /// Сохраняет тур в массив целых чисел, где каждый элемент будет номером узла. 
        /// </summary>
        public void SaveTourToArray(ref int[] tour)
        {
            Node n = firstNode;
            int i = 0;
            tour = new int[tour.Count()];
            do
            {
                tour[i] = n.Id;
                i++;
                n = n.Succ;
            } while (n != FirstNode);       
        }

        //------------------------------------------------------------------------------
        // Прочие методы.

        /// <summary>
        /// Заменяет все нули в матрице стоимости (кроме диагоналей) на очень большие числа.
        /// </summary>
        public void SetNullesToBigNumber()
        {
            if (CostMatrix != null)
            {
                for (int i = 0; i < costMatrix.Length; i++)
                {
                    for (int j = 0; j < costMatrix[i].Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        if (costMatrix[i][j] == 0)
                            costMatrix[i][j] = BigNumber;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает максимальное значение X координаты.
        /// </summary>
        public double GetMaxCoordX()
        {
            double maxX = double.MinValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.X > maxX)
                    maxX = nodesList[i].Coords.X;
            }
            return maxX;
        }

        /// <summary>
        /// Возвращает максимальное значение Y координаты.
        /// </summary>
        public double GetMaxCoordY()
        {
            double maxY = double.MinValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.Y > maxY)
                    maxY = nodesList[i].Coords.Y;
            }
            return maxY;
        }

        /// <summary>
        /// Возвращает максимальное значение Z координаты.
        /// </summary>
        public double GetMaxCoordZ()
        {
            double maxZ = double.MinValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.Z > maxZ)
                    maxZ = nodesList[i].Coords.Z;
            }
            return maxZ;
        }

        /// <summary>
        /// Возвращает минимальное значение X координаты.
        /// </summary>
        public double GetMinCoordX()
        {
            double minX = double.MaxValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.X < minX)
                    minX = nodesList[i].Coords.X;
            }
            return minX;
        }

        /// <summary>
        /// Возвращает минимальное значение Y координаты
        /// </summary>
        public double GetMinCoordY()
        {
            double minY = double.MaxValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.Y < minY)
                    minY = nodesList[i].Coords.Y;
            }
            return minY;
        }

        /// <summary>
        /// Возвращает минимальное значение Z координаты.
        /// </summary>
        public double GetMinCoordZ()
        {
            double minZ = double.MaxValue;
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (nodesList[i].Coords.Z < minZ)
                    minZ = nodesList[i].Coords.Z;
            }
            return minZ;
        }
    }

}