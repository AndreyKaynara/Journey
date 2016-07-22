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

namespace JourneyExceptions
{

    /*
     * Список всех возможных специфических исключений и их обработчиков, которые могут возникнуть
     * при работе программы, а также кодов ошибок для них и других исключений.
     */

    public class ErrorCodes
    {
        // Код успеха, означает, что ошибки не произошло.
        public const string Success = "0x000";
        // Неизвестная ошибка.
        public const string UnknownError = "0x001";
        // Ошибка ввода-вывода.
        public const string IOError = "0x002";
        // Неверный формат файла проекта (неверное значение параметра, неверный параметр или же неверен сам формат файла).
        public const string InvalidProjectFileError = "0x003";
        // Неверный формат файла TSPLib.
        public const string InvalidTSPLibFileError = "0x004";
        // Вызов нереализованного функционала.
        public const string NotImplementedTSPError = "0x005";
        // Неинициализированный объект. 
        public const string ObjectIsNotInitialized = "0x006";
        // Ошибка при работе NodesList. 
        public const string NodesListError = "0x007";
        // Ошибка при работе EdgesList.
        public const string EdgesListError = "0x008";
        // Ошибка при загрузке тура.
        public const string InvalidTourTSP = "0x009";
        // Ошибка при построении тура. 
        public const string DrawMapError = "0x00A";
        // Ошибка при построении случайного тура.
        public const string RandomTourError = "0x100";
        // Ошибка при построении тура по алгоритму ближайшего соседа.
        public const string NearestNeighborError = "0x200";
        // Ошибка при работе 2-опт алгоритма. 
        public const string TwoOptError = "0x300";
        // Ошибка при работе алгоритма Лина-Кернигана.
        public const string LinKernighanError = "0x400"; 
    }

    /// <summary>
    /// Не был указан обязательный параметр.
    /// </summary>
    [Serializable]
    public class ProblemFileIsNotSpecifiedException : Exception
    {
        public ProblemFileIsNotSpecifiedException() { }
        public ProblemFileIsNotSpecifiedException(string message) : base(message) { }
        public ProblemFileIsNotSpecifiedException(string message, Exception inner) : base(message, inner) { }
        protected ProblemFileIsNotSpecifiedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// Неверный формат ключа параметра.
    /// </summary>
    [Serializable]
    public class InvalidParameterKeyException : Exception
    {
        public InvalidParameterKeyException() { }
        public InvalidParameterKeyException(string message) : base(message) { }
        public InvalidParameterKeyException(string message, Exception inner) : base(message, inner) { }
        protected InvalidParameterKeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// Неверный формат значения параметра.
    /// </summary>
    [Serializable]
    public class InvalidParameterValueException : Exception
    {
        public InvalidParameterValueException() { }
        public InvalidParameterValueException(string message) : base(message) { }
        public InvalidParameterValueException(string message, Exception inner) : base(message, inner) { }
        protected InvalidParameterValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// Неизвестное ключевое слово в файле TSPLib.
    /// </summary>
    [Serializable]
    public class UnknownProblemKeywordException : Exception
    {
        public UnknownProblemKeywordException() { }
        public UnknownProblemKeywordException(string message) : base(message) { }
        public UnknownProblemKeywordException(string message, Exception inner) : base(message, inner) { }
        protected UnknownProblemKeywordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    
    /// <summary>
    /// Неверное значение параметра файла TSPLib.
    /// </summary>
    [Serializable]
    public class InvalidTSPLibValueException : Exception
    {
        public InvalidTSPLibValueException() { }
        public InvalidTSPLibValueException(string message) : base(message) { }
        public InvalidTSPLibValueException(string message, Exception inner) : base(message, inner) { }
        protected InvalidTSPLibValueException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// Ошибка при обращении к нереализованной спецификации TSPLib.
    /// </summary>
    [Serializable]
    public class NotImplementedTSPException : Exception
    {
        public NotImplementedTSPException() { }
        public NotImplementedTSPException(string message) : base(message) { }
        public NotImplementedTSPException(string message, Exception inner) : base(message, inner) { }
        protected NotImplementedTSPException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    /// <summary>
    /// Исключение при работе с неинициализированным объектом. 
    /// </summary>
    [Serializable]
    public class ObjectIsNotInitializedException : Exception
    {
        public ObjectIsNotInitializedException() { }
        public ObjectIsNotInitializedException(string message) : base(message) { }
        public ObjectIsNotInitializedException(string message, Exception inner) : base(message, inner) { }
        protected ObjectIsNotInitializedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
    
    /// <summary>
    /// Ошибка при работе NodesList. 
    /// </summary>
    [Serializable]
    public class NodesListException : Exception
    {
        public NodesListException() { }
        public NodesListException(string message) : base(message) { }
        public NodesListException(string message, Exception inner) : base(message, inner) { }
        protected NodesListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }


    /// <summary>
    /// Ошибка при работе EdgesList. 
    /// </summary>
    [Serializable]
    public class EdgesListException : Exception
    {
        public EdgesListException() { }
        public EdgesListException(string message) : base(message) { }
        public EdgesListException(string message, Exception inner) : base(message, inner) { }
        protected EdgesListException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при обработке файла с туром.
    /// </summary>
    [Serializable]
    public class InvalidTSPTourException : Exception
    {
        public InvalidTSPTourException() { }
        public InvalidTSPTourException(string message) : base(message) { }
        public InvalidTSPTourException(string message, Exception inner) : base(message, inner) { }
        protected InvalidTSPTourException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при отрисовке тура на карте.
    /// </summary>
    [Serializable]
    public class DrawMapException : Exception
    {
        public DrawMapException() { }
        public DrawMapException(string message) : base(message) { }
        public DrawMapException(string message, Exception inner) : base(message, inner) { }
        protected DrawMapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при построении случайного тура.
    /// </summary>
    [Serializable]
    public class RandomTourErrorException : Exception
    {
        public RandomTourErrorException() { }
        public RandomTourErrorException(string message) : base(message) { }
        public RandomTourErrorException(string message, Exception inner) : base(message, inner) { }
        protected RandomTourErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при работе алгоритма ближайшего соседа.
    /// </summary>
    [Serializable]
    public class NearestNeighborErrorException : Exception
    {
        public NearestNeighborErrorException() { }
        public NearestNeighborErrorException(string message) : base(message) { }
        public NearestNeighborErrorException(string message, Exception inner) : base(message, inner) { }
        protected NearestNeighborErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при работе алгоритма 2-опт.
    /// </summary>
    [Serializable]
    public class TwoOptErrorException : Exception
    {
        public TwoOptErrorException() { }
        public TwoOptErrorException(string message) : base(message) { }
        public TwoOptErrorException(string message, Exception inner) : base(message, inner) { }
        protected TwoOptErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

    /// <summary>
    /// Ошибка при работе алгоритма Лина-Кернигана. 
    /// </summary>
    [Serializable]
    public class LinKernighanErrorException : Exception
    {
        public LinKernighanErrorException() { }
        public LinKernighanErrorException(string message) : base(message) { }
        public LinKernighanErrorException(string message, Exception inner) : base(message, inner) { }
        protected LinKernighanErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }

}