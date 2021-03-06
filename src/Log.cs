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
using System.IO;

using System.Windows.Forms;

/* 
 * Класс Logs предоставляет возможность записи в выходной поток строковой информации. 
 * В текущей реализации выходным потоком является объект класса TextBox. 
 * Он используется в различных методах по всей программе. При необходимости, 
 * вывод логов всегда можно отключить, закомментировав код публичного метода Notify.
 * Или же передалать выход в строковый файл или консоль (в случае реализации конcольного приложения).
 */


namespace JourneyLogs
{
    /// <summary>
    /// Класс Logs предоставляет возможность записи в выходной поток строковой информации. 
    /// </summary>
    class Logs
    {
        public static TextBox Output; // Текстовое поле, куда писать записи.
        public static int LogLevel = 1; // Заданный уровень логгирования.

        /// <summary>
        /// Выводит сообщение в выходной поток. Вторым параметром указывается уровень сообщения (меньше — важнее).
        /// </summary>
        public static void Notify(string str, int logLevel = 255)
        {
            // Пишем только если переданный уровень логгирования меньше или равен установленному.
            if (logLevel <= LogLevel)
            {
                Notify(str);
            }
        }

        /// <summary>
        /// Добавляет горизонтальный разделитель в выходной поток. 
        /// </summary>
        public static void AddSeparator()
        {
            Notify("--------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Добавляет массивный горизонтальный разделитель в выходной поток. 
        /// </summary>
        public static void AddBigSeparator()
        {
            Notify("================================================================================");
        }

        /// <summary>
        /// Выводит сообщение в выходной поток. 
        /// </summary>
        private static void Notify(string str)
        {
            if (Output != null) 
            {
                Output.AppendText(Environment.NewLine + str);
            }
        }
    }
}