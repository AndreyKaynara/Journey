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
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using JourneyExceptions;


namespace JourneyIO
{
    /* 
     * Класс JourneyProject осуществляет хранение и обработку файла с проектом.
     * На вход поступает путь к текстовму файлу, в результате обработки которого
     * происходит инициализация полей класса параметрами.
     * 
     * Рекомендуется, чтобы файл с проектом имел расширение *.jproj.
     * 
     * Параметры хранятся в XML файле в узле Parameters главного узла JourneyProject.  
     * Каждого параметру соответсвует один узел. Значению параметра соответствует буквенные 
     * или численные данные. Ключевые слова чувствительны к регистру, содержимое узлов — нет.
     * 
     * Порядок параметров значение не имеет. 
     *
     * Следующий параметр обязателен:
     * 
     * <ProblemFile> [string] </ProblemFile>
     * Определяет имя файла с задачей.
     * 
     * Дополнительные параметры необязательны и в случае их отсутствия будут заменены значениями
     * по умолчанию.
     * 
     * Они задаются в следующем формате:
     * 
     * <LogLevel> [integer] </LogLevel>
     * Минимум: 0
     * Определяет уровень детализации вывода информации в течении работы программы.
     * По умолчанию: 1.
     * 
     * <Optimum> [real] </Optimum>
     * Минимум: > 0
     * Оптимальное ранее вычисленное значение длины тура. 
     * По умолчанию: плюс бесконечность. 
     *      
     * <TourFile> <string> </TourFile>
     * Определяет имя файла для быстрой записи и загрузки тура.
     * 
     * <RandomFirstNode> [YES | NO] </RandomFirstNode>
     * Указывает, нужно ли в качестве первого узла при расчёте случайного тура и 
     * работе алгоритма ближайщешего соседа использовать случайный узел.
     * По умолчанию: YES 
     *
     * <LKNearestNeighbors> <integer> </LKNearestNeighbors>
     * Минимум: 1
     * Задаёт количество ближайших соседей к узлу, которые будут просматривать при поиске ребра при работе
     * алгоритма Лина-Кернигана.
     * По умолчанию: 12
     *
     * <LKGap> [real] </LKGap>
     * Минимум: 0
     * Задаёт значение разницы выгоды между двумя найденными улучшениями тура при работе алгоритма
     * Лина-Кернигана. Если при расчётах новый тур будет лучше старого менее чем на эту разницу,
     * алгоритм прекращает работу.
     * По умолчанию: 0 
     *
     */

    /* В классе хранятся названия параметров. */
    class ParametersNames
    {
        public const string LogLevel = "LogLevel";    
        public const string Optimum = "Optimum";
        public const string ProblemFile = "ProblemFile";
        public const string TourFile = "TourFile";
        public const string RandomFirstNode = "RandomFirstNode";
        public const string LKNearestNeighbors = "LKNearestNeighbors";
        public const string LKGap = "LKGap";
    }

    /// <summary>
    /// Обеспечивает обрабутку файла с проектом и хранение данных из него.
    /// </summary>
    public class JourneyProject
    {
        // Поля класса.
        private string projectFileName;
        private string problemFileName, tourFileName;
        private int logLevel;
        private double optimum;
        private bool randomFirstNode;
        private int lkNearestNeighbors;
        private double lkGap;


        //==============================================================================
        // Свойства класса.
        public string ProjectFileName
        {
            get { return projectFileName; }
        }
        public string ProblemFileName
        {
            get { return problemFileName; }
            set { problemFileName = value; }
        }
        public string TourFileName
        {
            get { return tourFileName; }
            set { tourFileName = value; }
        }

        public int LogLevel
        {
            get { return logLevel; }
            set
            {
                if (value < 0)
                    throw new InvalidParameterValueException(ParametersNames.LogLevel + ": ожидалось целое число >= 0.");
                logLevel = value;
            }
        }
        public double Optimum
        {
            get { return optimum; }
            set
            {
                if (value <= 0)
                    throw new InvalidParameterValueException(ParametersNames.Optimum + ": ожидалось вещественное число больше нуля.");
                optimum = value;
            }
        }

        public bool RandomFirstNode
        {
            get { return randomFirstNode; }
            set { randomFirstNode = value; }
        }

        public int LKNearestNeighbors
        {
            get { return lkNearestNeighbors; }
            set
            {
                if (value < 1)
                    throw new InvalidParameterValueException(ParametersNames.LKNearestNeighbors + ": ожидалось целое число >= 1.");
                lkNearestNeighbors = value;
            }
        }

        public double LKGap
        {
            get { return lkGap; }
            set
            {
                if (value < 0)
                    throw new InvalidParameterValueException(ParametersNames.LKGap + ": ожидалось вещественное число больше или равное нулю.");
                lkGap = value;
            }
        }

        //==============================================================================
        // Публичные методы.

        /* Конструктор класса. */
        public JourneyProject(string fileName)
        {
            SetDefaultParameters(true);
            projectFileName = fileName;
            ReadProject(fileName);
        }

        public JourneyProject()
        {
            SetDefaultParameters(true);
        }

        /* Инициализация параметров значениями по умолчанию. */
        public void SetDefaultParameters(bool clearProblemFile)
        {
            if (clearProblemFile)
                problemFileName = "";
            tourFileName = "";
            logLevel = 1;
            optimum = double.PositiveInfinity;
            randomFirstNode = true;
            lkNearestNeighbors = 12;
            lkGap = 0;
        }

        /* Преобразование всех параметров в строковой вид. */
        public string ParametersToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(ParametersNames.LogLevel + " = " + this.logLevel.ToString());
            str.AppendLine(ParametersNames.Optimum + " = " + this.optimum.ToString());
            str.AppendLine(ParametersNames.ProblemFile + " = " + this.problemFileName);
            str.AppendLine(ParametersNames.RandomFirstNode + " = " + this.randomFirstNode.ToString());
            str.AppendLine(ParametersNames.TourFile + " = " + this.tourFileName);
            str.AppendLine(ParametersNames.LKNearestNeighbors + " = " + this.LKNearestNeighbors);
            str.AppendLine(ParametersNames.LKGap + " = " + this.LKGap);
            return str.ToString();
        }

        /* Сохранения параметров проекта в файл. */
        public void SaveProject(string fileName)
        {
            string strRandomFirstNode;
            if (this.randomFirstNode)
                strRandomFirstNode = "yes";
            else
                strRandomFirstNode = "no";
            XElement doc =
                new XElement("JourneyProject",
                    new XElement("Parameters",
                        new XElement(ParametersNames.ProblemFile, this.problemFileName),
                        new XElement(ParametersNames.LogLevel, this.logLevel.ToString()),
                        new XElement(ParametersNames.Optimum, this.optimum.ToString()),
                        new XElement(ParametersNames.RandomFirstNode, strRandomFirstNode),
                        new XElement(ParametersNames.TourFile, this.tourFileName),
                        new XElement(ParametersNames.LKNearestNeighbors, this.LKNearestNeighbors),
                        new XElement(ParametersNames.LKGap, this.LKGap)
                    )
            );
            doc.Save(fileName);
        }

        /* Главная функция по чтению и обработке параметров из файла. */
        public int ReadProject(string fileName)
        {
            projectFileName = fileName;
            XDocument parFile = null;
            int lineNum = 0; // Счётчик строк.
            IEnumerable<XElement> pars = null;

            parFile = XDocument.Load(fileName, LoadOptions.SetLineInfo);
            foreach (XElement el in parFile.Root.Elements())
            {
                    
                if (el.Name.ToString() == "Parameters")
                {
                    pars = el.Elements();
                }
            }
            foreach (XElement el in pars)
            {
                lineNum = ((IXmlLineInfo)el).LineNumber;
                RecognizeKeyAndSetValue(el);
            }
                
            if (ProblemFileName == "")
                throw new ProblemFileIsNotSpecifiedException("Не был указан файл с задачей.");
           
            return 0;
        }

        /* Преобразовывает строковое представление вещественного числа в нормальный вид с точки зрения региональных стандартов. */ 
        private void FormatRealNumber(ref string num)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
            num = num.Replace(',', separator);
            num = num.Replace('.', separator);
        }

        /* Анализирует строку на значение "YES" или "NO" и возвращает 1 в случае "YES", 0 в случае "NO" и -1 в ином случае. */ 
        private int ReadYesOrNo(string strLine)
        {
            strLine = strLine.ToUpper();
            if ((strLine == "YES") || (strLine == "Y"))
                return 1;
            if ((strLine == "NO") || (strLine == "N"))
                return 0;
            return -1;
        }

        /* Обрабатывает переданный элемент и задаёт значение нужному свойству. */ 
        private void RecognizeKeyAndSetValue(XElement element)
        {
            string key = element.Name.ToString(); // Ключ.
            string value = element.Value.ToString(); // Значение.
            int res = 0;
            // Важно присваивать значения свойствам, а не полям — при этом во многих случаях производятся дополнительные проверки.
            switch (key)
            {
                case ParametersNames.LogLevel:
                    int logLevel;
                    if (!int.TryParse(value, out logLevel))
                        throw new InvalidParameterValueException(ParametersNames.LogLevel + ": ожидалось целое число.");
                    LogLevel = logLevel;
                    break;
                case ParametersNames.Optimum:
                    double optimum;
                    FormatRealNumber(ref value);
                    if (!double.TryParse(value, out optimum))
                        throw new InvalidParameterValueException(ParametersNames.Optimum + ": ожидалось вещественное число.");
                    Optimum = optimum;
                    break;
                case ParametersNames.ProblemFile:
                    ProblemFileName = value;
                    break;
                case ParametersNames.RandomFirstNode:
                    res = ReadYesOrNo(value);
                    if (res == -1)
                        throw new InvalidParameterValueException(ParametersNames.RandomFirstNode + ": Ожидалось YES или NO.");
                    randomFirstNode = Convert.ToBoolean(res);
                    break;
                case ParametersNames.TourFile:
                    tourFileName = value;
                    break;
                case ParametersNames.LKNearestNeighbors:
                    int nn;
                    if (!int.TryParse(value, out nn))
                        throw new InvalidParameterValueException(ParametersNames.LKNearestNeighbors + ": ожидалось целое число.");
                    LKNearestNeighbors = nn;
                    break;
                case ParametersNames.LKGap:
                    double lkGap;
                    FormatRealNumber(ref value);
                    if (!double.TryParse(value, out lkGap))
                        throw new InvalidParameterValueException(ParametersNames.LKGap + ": ожидалось вещественное число.");
                    LKGap = lkGap;
                    break;
                default:
                    throw new InvalidParameterKeyException("Ошибка! Обнаружен неизвестный ключ " + key);
            }
        }

    }
}
