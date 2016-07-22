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
    /// Вычисление тура с помощью 2-opt алгоритма.
    /// </summary>
    class TwoOptSolver
    {
        /// <summary>
        /// Вычисляет тур с помощью 2-opt алгоритма и возвращает его стоимость.
        /// </summary>
        static public double Solve(NodesList nodesList)
        {
            // Получаем размер задачи. 
            int size = nodesList.Dimension;

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
            int improve = 0;
            double cost, newCost = 0;
            double gain = 0;

            // Делаем обмены, пока есть улучшения.
            while (improve < 2)
            {

                for (int i = 0; i < size - 1; i++)
                {
                    for (int k = i + 1; k < size; k++)
                    {
                        if ((nodesList.ElementAt(i).Pred == nodesList.ElementAt(k)) || (nodesList.ElementAt(i) == nodesList.ElementAt(k).Succ))
                            continue;

                        // Считаем потенциальную выгоду от обмена.
                        cost = nodesList.ElementAt(i).Pred.Costs[nodesList.ElementAt(i).Id - 1] + nodesList.ElementAt(k).Succ.Costs[nodesList.ElementAt(k).Id - 1];
                        newCost = nodesList.ElementAt(i).Pred.Costs[nodesList.ElementAt(k).Id - 1] + nodesList.ElementAt(i).Costs[nodesList.ElementAt(k).Succ.Id - 1];

                        gain = cost - newCost;

                        // Если выгода есть, делаем 2-опт обмен, обращая часть тура в обратный порядок.
                        if (gain > 0)
                        {
                            nodesList.Reverse(nodesList.ElementAt(i), nodesList.ElementAt(k));
                            // Сбрасываем счётчик.
                            improve = 0;
                        }
                    }
                }
                improve++;
            }

            double tourCost = nodesList.Cost;

            if (tourCost < nodesList.BestCost)
                nodesList.BestCost = tourCost;

            return tourCost;
        }
    }
}
