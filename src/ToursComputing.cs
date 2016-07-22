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
    /// Решение задачи коммивояжёра с помощью различных алгоритмов. 
    /// </summary>
    class ToursComputing
    {
        /// <summary>
        /// Генерирует случайный тур и возвращает его стоимость.
        /// </summary>
        static public double RandomTour(NodesList nodesList, bool randomFirstNode)
        {
            return RandomTourSolver.Solve(nodesList, randomFirstNode);
        }

        //==============================================================================

        static public double NearestNeighbor(NodesList nodesList, bool randomFirstNode)
        {
            return NearestNeighborSolver.Solve(nodesList, randomFirstNode);
        }

        //==============================================================================

        /// <summary>
        /// Вычисление тура с помощью 2-opt алгоритма.
        /// </summary>
        static public double TwoOpt(NodesList nodesList)
        {
            return TwoOptSolver.Solve(nodesList);
        }

        //==============================================================================

        /// <summary>
        /// Вычисление тура с помощью алгоритма Лина-Кернигана. 
        /// </summary>
        public static double LinKernighan(NodesList nodesList, int nearestNeighborsCount, double gapValue)
        {
            LinKernighanSolver lkSolver = new LinKernighanSolver(nodesList, nearestNeighborsCount, gapValue);
            return lkSolver.Solve();
        }

    }


}