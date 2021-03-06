﻿/*
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
    /// Формирование случайного тура.
    /// </summary>
    static class RandomTourSolver
    {
        /// <summary>
        /// Формирует случайный тур и возвращает его стоимость.
        /// </summary>
        static public double Solve(NodesList nodesList, bool randomFirstNode = true)
        {
            // Список для идентификатов всех узлов.
            List<int> nodes = new List<int>();

            // Генератор псевдослучайных чисел.
            Random random = new Random();

            // Если был передан параметр, выбираем случайный узел и назначаем в качестве первого. 
            if (randomFirstNode)
            {
                nodesList.FirstNode = nodesList.ElementAt(random.Next(0, nodesList.Dimension));
            }

            Node a = nodesList.FirstNode; 
            int firstNodeIndex = a.Id;

            // Заполняем список идентификаторами городов. 
            for (int i = 0; i < nodesList.Dimension; i++)
            {
                // Не добавляем идентификатор первого узла. 
                if (i == a.Id - 1)
                    continue;
                nodes.Add(i);
            }

            // Пока в списке остаются города.
            while (nodes.Count != 0)
            {
                // Генерируем случайное число в диапазоне оставшихся узлов.
                int rnd = 0;
                int nodeID = 0;

                rnd = random.Next(0, nodes.Count); // Генерация случайного числа, нижний предел включён, верхний — исключён.
                // Выбираем соответствующий номер из оставшихся.
                nodeID = nodes[rnd];
                // Удаляем этот элемент из списка.
                nodes.RemoveAt(rnd);
                // Если связи нет, то переходим к следующему элементу.
                if (a.Costs != null)
                    if ((a.Costs[nodeID] == 0) || a.Costs[nodeID] == NodesList.BigNumber)
                        continue;
                // Производим перемещение в основном туре (ставим выбранный элемент после текущего).
                NodesList.Follow(nodesList.ElementAt(nodeID), a);
                // Перемещённый элемент становится текущим. 
                a = nodesList.ElementAt(nodeID);
            }

            double tourCost = nodesList.Cost;

            if (tourCost < nodesList.BestCost)
                nodesList.BestCost = tourCost;
            return tourCost;
        } 
    }
}
