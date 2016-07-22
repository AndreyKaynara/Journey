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
using System.Windows.Forms;

namespace JourneyExceptions
{
    /* 
     * Реализация обработки исключений в программе.
     * В текущей реализации вызывается окошко с сообщением об ошибке. 
     * При необходимости, можно сделать вывод в консоль или писать в файл. 
     */

    class ErrorHandle
    {
        /* Просто вывод сообщения. */
        static public void DoHandle(string message)
        {
            DoHandle(message, "Ошибка");
        }

        /* Вывод сообщения с возможностью задать заголовок. */
        static public void DoHandle(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /* Определение типа ошибки и вывод сообщения вместе с кодом ошибки. */
        static public void DoHandle(Exception e)
        {
            string errorCode;

            // Ошибка ввода-вывода.
            if (e is System.IO.IOException)
                errorCode = ErrorCodes.IOError;
            // Ошибки неверного формата файла проекта. 
            else if ((e is System.Xml.XmlException) || (e is InvalidParameterKeyException)
                || (e is InvalidParameterValueException) || (e is ProblemFileIsNotSpecifiedException))
                errorCode = ErrorCodes.InvalidProjectFileError;
            // Ошибки неверного формата файла TSPLib.
            else if ((e is UnknownProblemKeywordException) || (e is InvalidTSPLibValueException))
                errorCode = ErrorCodes.InvalidTSPLibFileError;
            // Вызов нереализованного функционала.
            else if (e is NotImplementedTSPException)
                errorCode = ErrorCodes.NotImplementedTSPError;
            // Неинициализированный объект.
            else if (e is ObjectIsNotInitializedException)
                errorCode = ErrorCodes.ObjectIsNotInitialized;
            // Ошибка при работе NodesList.
            else if (e is NodesListException)
                errorCode = ErrorCodes.NodesListError;
            // Ошибка при работе EdgesList.
            else if (e is EdgesListException)
                errorCode = ErrorCodes.EdgesListError;
            // Ошибка при чтении тура.
            else if (e is InvalidTSPTourException)
                errorCode = ErrorCodes.InvalidTourTSP;
            // Ошибка при построении карты.
            else if (e is DrawMapException)
                errorCode = ErrorCodes.DrawMapError;
            // Ошибка при построении случайного тура.
            else if (e is RandomTourErrorException)
                errorCode = ErrorCodes.RandomTourError;
            // Ошибка при работе алгоритма ближайшего соседа.
            else if (e is NearestNeighborErrorException)
                errorCode = ErrorCodes.NearestNeighborError;
            // Ошибка при работе 2-опт алгоритма.
            else if (e is TwoOptErrorException)
                errorCode = ErrorCodes.TwoOptError;
            // Ошибка при работе алгоритма Лина-Кернигана.
            else if (e is LinKernighanErrorException)
                errorCode = ErrorCodes.LinKernighanError;
            else
                errorCode = ErrorCodes.UnknownError;
           
            string message = errorCode + System.Environment.NewLine + e.Message;
            DoHandle(message);
        }
    }

}