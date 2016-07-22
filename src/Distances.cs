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

using System.Windows.Forms;

/* 
 *  Функции для вычисления расстояний. 
 *  Все функции имеют одинаковый интерфейс, что позволяет делигирование. 
 */

namespace JourneyTSP
{
    public delegate double Distance(Node nA, Node nB);

    public static class Distances
    {
        public static double Distance_1(Node nA, Node nB)
        {
            return 1;
        }

        public static double Distance_EXPLICIT(Node nA, Node nB)
        {
            return 0;
        }

        public static double Distance_CEIL_2D(Node nA, Node nB)
        {
            double xd = nA.Coords.X - nB.Coords.X, yd = nA.Coords.Y - nB.Coords.Y;
            return Math.Ceiling(Math.Sqrt(xd * xd + yd * yd));
        }

        public static double Distance_CEIL_3D(Node nA, Node nB)
        {
            double xd = nA.Coords.X - nB.Coords.X, yd = nA.Coords.Y - nB.Coords.Y, zd = nA.Coords.Z - nB.Coords.Z;
            return Math.Ceiling(Math.Sqrt(xd * xd + yd * yd + zd * zd));
        }

        public static double Distance_EUC_2D(Node nA, Node nB)
        {
            double xd = nA.Coords.X - nB.Coords.X, yd = nA.Coords.Y - nB.Coords.Y;
            return Math.Sqrt(xd * xd + yd * yd);
        }

        public static double  Distance_EUC_3D(Node nA, Node nB)
        {
            double xd = nA.Coords.X - nB.Coords.X, yd = nA.Coords.Y - nB.Coords.Y, zd = nA.Coords.Z - nB.Coords.Z;
            return Math.Sqrt(xd * xd + yd * yd + zd * zd);
        }

        public static double  Distance_MAN_2D(Node nA, Node nB)
        {
            return (Math.Abs(nA.Coords.X - nB.Coords.X) + Math.Abs(nA.Coords.Y - nB.Coords.Y));
        }

        public static double Distance_MAN_3D(Node nA, Node nB)
        {
            return (Math.Abs(nA.Coords.X - nB.Coords.X) + Math.Abs(nA.Coords.Y - nB.Coords.Y) + Math.Abs(nA.Coords.Z - nB.Coords.Z));
        }

        public static double Distance_MAX_2D(Node nA, Node nB)
        {
            int dx = (int) (Math.Abs(nA.Coords.X - nB.Coords.X) + 0.5),
                dy = (int) (Math.Abs(nA.Coords.Y - nB.Coords.Y) + 0.5);
            return dx > dy ? dx : dy;
        }

        public static double Distance_MAX_3D(Node nA, Node nB)
        {
            int dx = (int) (Math.Abs(nA.Coords.X - nB.Coords.X) + 0.5),
                dy = (int) (Math.Abs(nA.Coords.Y - nB.Coords.Y) + 0.5),
                dz = (int) (Math.Abs(nA.Coords.Z - nB.Coords.Z) + 0.5);
            if (dy > dx)
                dx = dy;
            return dx > dz ? dx : dz;
        }

        public static double Distance_ATT(Node nA, Node nB)
        {
            double xd = nA.Coords.X - nB.Coords.X, yd = nA.Coords.Y - nB.Coords.Y;
            return Math.Ceiling(Math.Sqrt((xd * xd + yd * yd) / 10.0));
        }

        public static double Distance_GEO(Node nA, Node nB)
        {
            // Переводим координаты из градусов в радианы.
            double at1 = nA.Coords.X * Math.PI / 180;
            double at2 = nB.Coords.X * Math.PI / 180;
            // Считаем угловую длину ортодромии. 
            double delta = Math.Acos(Math.Sin(at1) * Math.Sin(at2) +
                Math.Cos(at1) * Math.Cos(at2) * Math.Cos(nB.Coords.Y * Math.PI / 180 - nA.Coords.Y * Math.PI / 180));
            // Переводим обратно из радианов в градусы.
            delta *= 180 / Math.PI; 
            // l = длина дуги 1° меридиана (на Земле l = 111.1 км).
            double l = 111.1;
            // Считаем длину ортодромии.
            return l * delta;
        }

    }
}
