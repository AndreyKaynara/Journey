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
    /// 
    /// </summary>
    static class NearestNeighborSolver
    {
        /// <summary>
        /// Вычисляет тура алгоритмом ближайшего соседа и возвращает его стоимость.
        /// </summary>
        static public double Solve(NodesList nodesList, bool randomFirstNode = true)
        {
            double cost = 0, currentCost = 0; // Стоимость тура.
            int visited = 0; // Счётчик посещённых городов.

            // Очищаем теги посещённых городов.
            nodesList.ClearTags();

            // Создаём матрицу стоимости, если не была создана ранее.
            if (nodesList.CostMatrix == null)
                nodesList.CreateCostMatrix();
            // Ассоциируем строки матрицы стоимости с узлами, если не было сделано ранее.
            if (!nodesList.IsCostsAssociates)
                nodesList.AssociateCosts();

            Random random = new Random();
            // Если был передан параметр, выбираем случайный узел и назначаем в качестве первого. 
            if (randomFirstNode)
            {
                nodesList.FirstNode = nodesList.ElementAt(random.Next(0, nodesList.Dimension));
            }

            Node currentNode = nodesList.FirstNode;
            Node nextNode = null;

            while (visited != nodesList.Dimension)
            {
                currentNode.Tag = 1;
                visited++;
                nextNode = FindNearestUnvisited(nodesList, currentNode, out currentCost);
                cost = cost + currentCost;
                if (nextNode == null)
                {
                    cost = cost + currentNode.Costs[0];
                    break;
                }
                else
                {
                    NodesList.Follow(nextNode, currentNode);
                } 
                currentNode = nextNode;
            }

            double tourCost = nodesList.Cost;

            if (tourCost < nodesList.BestCost)
                nodesList.BestCost = tourCost;

            return tourCost;
        }

        /// <summary>
        /// Находит ближайший к переданному узлу непосещённый узел. Возвращает его и стоимость к нему. 
        /// </summary>
        static private Node FindNearestUnvisited(NodesList nodesList, Node from, out double costToNode)
        {
            Node bestNode = null;
            double cost = double.MaxValue, bestCost = double.MaxValue;

            int i;

            for (i = 0; i < from.Costs.Count(); i++)
            {
                if ((from.Costs[i] != 0) && (nodesList.ElementAt(i).Tag == 0))
                {
                    cost = from.Costs[i];

                    if (bestCost > cost)
                    {
                        bestCost = cost;
                        bestNode = nodesList.ElementAt(i);
                    }
                }
            }
            costToNode = (bestCost == double.MaxValue) ? 0 : bestCost;

            return bestNode;
        }
    }
}
