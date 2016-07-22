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
using System.Globalization;

using JourneyExceptions;
using JourneyTSP;

/*    
 * Класс TSPLib обеспечивает обработку файла задачи и хранения её данных. 
 *
 * Ниже приведён перевод описания формата файла из официальной документации TSPLIB.
 *
 * Файл состоит из двух частей — раздела спецификации и раздела данных. 
 * Раздел спецификации содержит информацию о формате файла и его наполнении.
 * Раздел данных содержит явно заданные данные задачи.
 * 
 * I. Раздел спецификации
 *
 * Все записи в этом разделе должны соответствовать форме <ключевое слово> : <значение>, где
 * <ключевое слово> означает числобуквенное ключевое слово и <значение> означает
 * числобуквенные или численные данные. Термины <string>, <integer> и <real>
 * означают соответственно строковые, целые и вещественные данные.
 * Спецификация ключевых слов в файле произвольна (в общем), но должна быть
 * согласована: если какое-либо ключевое слово указано, вся необходимая информация
 * для его корректной интерпретации также должна быть представлена. Регистр ключевых слов и их значений 
 * свободный.
 * 
 * Ниже приведён список всех ключевых слов:
 *
 * NAME : <string>
 * Идентификатор файла с данными.
 * 
 * TYPE : <string>
 * Указывает тип данных. Возможны следующие типы:
 *  TSP          Данные для симметричный задачи коммивояжёра.
 *  ATSP         Данные для асимметричный задачи коммивояжёра.
 *  SOP          Данные для последовательно упорядоченной задачи.
 *  HCP          Данные задачи гамильтонова цикла.
 *  CVRP         Задача маршрутизации грузовых транспортных средств c ограниченной вместимостью.
 *  TOUR         Тур (последовательность посещённых узлов).
 *
 * COMMENT : <string>
 * Дополнительные комментарии (обычно имя помощника или имя создателя экземпляра задачи).
 * Может быть несколько строчек с комментариями (предваряющимся ключевым словом COMMENT). 
 *
 * DIMENSION : <integer>
 * Число узлов.
 * 
 * CAPACITY: <integer>
 * Вместимость грузовиков в CVRP задаче.
 * 
 * EDGE_WEIGHT_TYPE : <string>
 * Определяет, какие веса рёбр (или расстояния) приведены. Могут быть следующими:
 * CEIL_2D      Двухмерные округлённые евклидовы расстояния.
 * CEIL_3D      Трёхмерные округлённые евклидовы расстояния.
 * EUC_2D       Двухмерные евклидовы расстояния.
 * EUC_3D       Трёхмерные евклидовы расстояния.
 * EXPLICIT     Расстояния указаны в явном виде в соответствующем разделе.
 * GEO          Вес является географическим расстоянием в километрах. 
 *              Координаты задаются в форме ГГГ.ММ, где ГГГ — градусы и ММ — минуты.
 * MAN_2D       Двухмерные манхэттенские расстояния.
 * MAN_3D       Трёхмерные манхэттенские расстояния.
 * MAX_2D       Максимальные двухмерные расстояния. 
 * MAX_3D       Максимальные трёхмерные расстояния.
 * ATT          Специальная функция расстояния задач att48 и att532
 * XRAY1        Специальная функция расстояния для задач рентгеновской кристаллографии (версия 1).
 * XRAY2        Специальная функция расстояния для задач рентгеновской кристаллографии (версия 2).
 * SPECIAL      Особая функция расстояния, описанная где-либо ещё.

 * EDGE-WEIGHT_FORMAT : <string>
 * Описывает формат данных веса рёбер, если они заданы точно. Может быть следующим:
 * FUNCTION         Вес задан в виде функции (см. выше);
 * FULL_MATRIX      Вес задан в виде полной матрицы;
 * UPPER_ROW        Верхнетреугольная матрица
 *                      (по строкам без диагональных элементов) row-wise without diagonal entries)
 * LOWER_ROW        Нижнетреугольная матрица
 *                      (по строкам без диагональных элементов)     
 * UPPER_DIAG_ROW   Верхнетреугольная матрица
 *                      (по строкам с диагональными элементами)
 * LOWER_DIAG_ROW   Нижнетреугольная матрица
 *                      (по строкам с диагональными элементами)
 * UPPER_COL        Верхнетреугольная матрица
 *                      (по столбцам без диагональных элементов)
 * LOWER_COL        Нижнетреугольная матрица
 *                      (по столбцам без диагональных элементов)
 * UPPER_DIAG_COL   Верхнетреугольная матрица
 *                      (по столбцам с диагональными элементами)
 * LOWER_DIAG_COL   Нижнетреугольная матрица
 *                      (по столбцам с диагональными элементами)
 *
 * EDGE_DATA_FORMAT : <string>
 * Описывает формат задаваемых рёбр, если граф не завершён. 
 * EDGE_LIST    Граф задаётся как список рёбр.
 * ADJ_LIST     Граф задаётся как список смежности.
 *
 * NODE_COORD_TYPE : <string>
 * Определяет, сколько координат ассоциируются с каждым узлом 
 * (полезно, например, для графического отображения или расчёта расстояний).
 * Принимаемые значения:
 *  TWOD_COORDS      Узлы заданы 2D координатами.
 *  THREED_COORDS    Узлы заданы 3D координатами. 
 *  NO_COORDS        Узлы не имеют ассоциируемых координат. 
 * Значение по умолчанию NO_COORDS.
 *
 * DISPLAY_DATA_TYPE : <string
 * Указывает, как графическое отображение узлов может быть получено.
 * Принимаемые значения:
 * COORD_DISPLAY    Отображение генерируется из координат узлов.
 * TWOD_DISPLAY     Заданы явные двухмерные координаты.
 * NO_DISPLAY       Нет возможности графического отображения.
 *
 * Значением по умолчанию является COORD_DISPLAY если координаты узлов указаны и NO_DISPLAY в прочих случах.
 *
 * EOF
 * Завершает ввод данных. Запись опциональна.
 *
 * II. Раздел данных.
 *
 * В зависимости от указанной информации в разделе спецификации, некоторые 
 * дополнительные данные могут потребоваться. Эти данные задаются в соответствующем разделе после 
 * части спецификации. Каждый раздел данных начинается с определённого ключевого слова. 
 * Длина раздела зависит от заданных параметров раздела спецификации, или же заканчивается 
 * специальным идентификатором.
 *
 * NODE_COORD_SECTION :
 * Координаты узлов указываются в этом разделе. Каждая линия соответствует формату
 *   <integer> <real> <real>
 * если NODE_COORD_TYPE задан как TWOD_COORDS, или 
 *   <integer> <real> <real> <real>
 * если NODE_COORD_TYPE задан как THREED_COORDS. Целые числа являются идентификаторами узлов, 
 * вещественные числа — координаты.
 * 
 * DEPOT_SECTION :
 * Содержит список возможных альтернативных депо-узлов. Список заканчивается -1.
 *
 * DEMAND_SECTION :
 * Требования всех узлов CVRP задаются в форме
 *   <integer> <integer>
 * Первое целое число означает номер узла, следующее — требование. Узлы-депо также должны быть в этом разделе.
 * Их требование равно 0.
 * 
 * EDGE_DATA_SECTION :
 * Рёбра графа задаются в одном из двух форматах, указанных в ключе 
 * EDGE_DATA_FORMAT. Если тип — EDGE_LIST, тогда рёбра задаются как последовательность
 * строк вида:
 *  
 *      <integer> <integer>
 *
 * каждая запись представляет два конечных узла одного ребра. Список заканчивается -1.
 * Если тип ADJ_LIST, раздел содержит списки смежных узлов. 
 * Список указаывается как:
 * 
 *      <integer> <integer> ... <integer> -1
 *       
 * где первое целое показывает номер узла x, и последующие целые (заканчивающиеся -1)
 * числа показывает номера узлов, соседних к x. Список заканчивается дополнительной -1.
 *
 * FIXED_EDGES_SECTION :
 * В этом разделе указываются рёбра, которые должны быть в каждом решении проблемы.
 * Они задаются в виде:
 * 
 *      <integer> <integer>
 * 
 * означающим, что каждое ребро от первого узла до второго, должно быть в решении.      
 *
 * DISPLAY_DATA_SECTION :
 * Если DISPLAY_DATA_TYPE равен TWOD_DISPLAY, двумерные координаты, по которым отображение 
 * узлов может быть сегнерировано предоставляется в следующем формате (построчно): 
 *  
 *      <integer> <real> <real>
 *      
 * Целые числа представляют собой узлы. Вещественные числа являются соответствующими координатами.
 * 
 * TOUR_SECTION :
 * В этом разделе указывается в тур (список посещённых городов). Каждый тур задаётся списком целых чисел,
 * отражающих последовательность узлов, посещённых в туре. Он заканчивается -1.
 *
 * EDGE_WEIGHT_SECTION :
 * В разделе указываются веса рёбр в формате, указанном в параметре EDGE_WEIGHT_FORMAT.
 * Записи представляют собой строки матрицы, каждый элемент пишется через пробел и может 
 * быть целым или вещественным числом.
 * 
 */

namespace JourneyIO
{

    /// <summary>
    /// Тип задачи.
    /// </summary>
    public enum TSPType
    {
        TSP,
        ATSP,
        SOP,
        HCP,
        CVRP,
        TOUR
    }

    /// <summary>
    /// Тип данных задачи.
    /// </summary>
    public enum TSPEdgeWeightType
    {
        ATT,
        CEIL_2D,
        CEIL_3D,
        EUC_2D,
        EUC_3D,
        EXPLICIT,
        GEO,
        MAN_2D,
        MAN_3D,
        MAX_2D,
        MAX_3D,
        XRAY1,
        XRAY2,
        SPECIAL
    }

    /// <summary>
    /// Формат данных задачи.
    /// </summary>
    public enum TSPEdgeWeightFormat
    {
        FUNCTION,
        FULL_MATRIX,
        UPPER_ROW,
        LOWER_ROW,
        UPPER_DIAG_ROW,
        LOWER_DIAG_ROW,
        UPPER_COL,
        LOWER_COL,
        UPPER_DIAG_COL,
        LOWER_DIAG_COL 
    }

    /// <summary>
    /// Формат данных рёбер.
    /// </summary>
    public enum TSPEdgeDataFormat
    {
        EDGE_LIST,   
        ADJ_LIST     
    }

    /// <summary>
    /// Тип координат.
    /// </summary>
    public enum TSPCoordType
    {
        NO_COORDS = 0,
        TWOD_COORDS = 2,
        THREED_COORDS = 3   
    }

    /// <summary>
    /// Тип отображаемых данных.
    /// </summary>
    public enum TSPDisplayDataType
    {
        COORD_DISPLAY,
        TWOD_DISPLAY,   
        NO_DISPLAY       
    }

    /// <summary>
    /// Структура для хранения данных тура.
    /// </summary>
    public struct TSPTour
    {
        public string Name;
        public string Comment;
        public int Dimension;
        public TSPType Type;
        public int[] Tour;
    }

    /// <summary>
    /// Класс предоставляет возможности для работы с файлами формата TSPLib — их обработку и доступ к прочитанным данным.
    /// </summary>
    public class TSPLib
    {
        public struct DisplayDataEl
        {
            public double x, y;
            public DisplayDataEl(double p1, double p2)
            {
                x = p1;
                y = p2;
            }
        }

        // Поля класса.
        private Boolean isEOF = false; // Флаг, устанавливающийся в true при считывании ключевого слова "EOF" в одной из внутренних функций.
        private string name = "";
        private string comment = "";
        private int dimension = 0;
        private int capacity = 0; 
        private TSPType problemType;
        private TSPCoordType coordType = TSPCoordType.NO_COORDS;
        private TSPEdgeWeightType weightType = TSPEdgeWeightType.EUC_2D;
        private TSPEdgeWeightFormat weightFormat = TSPEdgeWeightFormat.FULL_MATRIX;
        private Distance distance = Distances.Distance_1;
        private TSPEdgeDataFormat ? edgeDataFormat = null;
        private TSPDisplayDataType displayDataType = TSPDisplayDataType.NO_DISPLAY;
        private NodesList nodes = null;
        private List<int> depots = new List<int>(); 
        private List<int> demands = new List<int>();
        private List<DisplayDataEl> displayData = new List<DisplayDataEl>();


        //==============================================================================
        // Публичные методы.
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public int Dimension
        {
            get { return dimension; }
            set
            {
                if (value < 2)
                    throw new InvalidTSPLibValueException("DIMENSION: значение должно быть целым числом больше или равное двум.");
                dimension = value;
            }
        }
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value <= 0)
                    throw new InvalidTSPLibValueException("CAPACITY: ожидалось целое число больше нуля.");
                capacity = value;
            }
        }
        public TSPType ProblemType
        {
            get { return problemType; }
            set { problemType = value; }
        }
        public TSPCoordType CoordType
        {
            get { return coordType; }
            set { coordType = value; }
        }
        public TSPEdgeWeightType WeightType
        {
            get { return weightType; }
            set { weightType = value; }
        }
        public TSPEdgeWeightFormat WeightFormat
        {
            get { return weightFormat; }
            set { weightFormat = value; }
        }
        public Distance Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public TSPEdgeDataFormat ? EdgeDataFormat
        {
            get { return edgeDataFormat; }
            set { edgeDataFormat = value; }
        }
        public TSPDisplayDataType DisplayDataType
        {
            get { return displayDataType; }
            set { displayDataType = value; }
        }
        public NodesList Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }        
        public List<int> Depots
        {
            get { return depots; }
            set { depots = value; }
        }
        public List<int> Demands
        {
            get { return demands; }
            set { demands = value; }
        }
        public List<DisplayDataEl> DisplayData
        {
            get { return displayData; }
            set { displayData = value; }
        }


        // Конструктор класса.
        public TSPLib(string fileName)
        {
            ReadProblem(fileName);
        }

        //==============================================================================
        // Публичные методы.

        /// <summary>
        /// Главный метод класса. Строка за строкой читает и обрабатывает файл задачи. 
        /// </summary>
        public void ReadProblem(string fileNameTSP)
        {
            isEOF = false;
            StreamReader problemFile = null;
            int lineNum = 1; // Счётчик строк.
            string curLine = ""; // Текущая строка.
            string keyword = ""; // Ключевое слово.
            try
            {
                problemFile = new StreamReader(fileNameTSP);
                while (!problemFile.EndOfStream)
                {
                    curLine = problemFile.ReadLine();
                    if (curLine.Trim() == "")
                    {
                        lineNum++;
                        continue;
                    }
                    keyword = GetKeyword(curLine).ToUpper();
                    if (isEOF == true)
                    {
                        isEOF = false;
                        return;
                    }
                    switch (keyword)
                    {
                        case "EOF":
                            return;
                        case "NAME":
                            Read_NAME(curLine);
                            break;
                        case "TYPE":
                            Read_TYPE(curLine);
                            break;
                        case "COMMENT":
                            Read_COMMENT(curLine);
                            break;
                        case "DIMENSION":
                            Read_DIMENSION(curLine);
                            break;
                        case "CAPACITY":
                            Read_CAPACITY(curLine);
                            break;
                        case "EDGE_WEIGHT_TYPE":
                            Read_EDGE_WEIGHT_TYPE(curLine);
                            break;
                        case "EDGE_WEIGHT_FORMAT":
                            Read_EDGE_WEIGHT_FORMAT(curLine);
                            break;
                        case "EDGE_DATA_FORMAT":
                            Read_EDGE_DATA_FORMAT(curLine);
                            break;
                        case "NODE_COORD_TYPE":
                            Read_NODE_COORD_TYPE(curLine);
                            break;
                        case "DISPLAY_DATA_TYPE":
                            Read_DISPLAY_DATA_TYPE(curLine);
                            break;
                        case "NODE_COORD_SECTION":
                            Read_NODE_COORD_SECTION(problemFile, ref lineNum);
                            break;
                        case "DEPOT_SECTION":
                            Read_DEPOT_SECTION(problemFile, ref lineNum);
                            break;
                        case "DEMAND_SECTION":
                            Read_DEMAND_SECTION(problemFile, ref lineNum);
                            break;
                        case "EDGE_DATA_SECTION":
                            Read_EDGE_DATA_SECTION();
                            break;
                        case "FIXED_EDGES_SECTION":
                            Read_FIXED_EDGES_SECTION();
                            break;
                        case "DISPLAY_DATA_SECTION":
                            Read_DISPLAY_DATA_SECTION(problemFile, ref lineNum);
                            break;
                        case "EDGE_WEIGHT_SECTION":
                            Read_EDGE_WEIGHT_SECTION(problemFile, ref lineNum);
                            nodes.AssociateCosts();
                            break;
                        default:
                            throw new UnknownProblemKeywordException("Неизвестное ключевое слово \"" + keyword + "\".");
                    }
                    lineNum++;
                }
            }
            catch (UnknownProblemKeywordException e)
            {
                // Перевозбуждаем исключение, дополнительно сообщив номер строки.
                throw new UnknownProblemKeywordException("Строка " + lineNum + ":" + Environment.NewLine + e.Message);
            }
            catch (InvalidTSPLibValueException e)
            {
                // Перевозбуждаем исключение, дополнительно сообщив номер строки.
                throw new InvalidTSPLibValueException("Строка " + lineNum + ":" + Environment.NewLine + e.Message);
            }            

            finally
            {
                if (problemFile != null)
                        problemFile.Close();
            }
        }

        public TSPTour ReadTour(string fileNameTour)
        {
            isEOF = false;
            TSPTour tour = new TSPTour();
            StreamReader tourFile = null;
            int lineNum = 1; // Счётчик строк.
            string curLine = ""; // Текущая строка.
            string keyword = ""; // Ключевое слово.
            // Записываем старые значения TSP, после обработки тура вернём их.
            string tspName = name;
            string tspComment = comment;
            comment = "";
            int tspDim = dimension;
            TSPType tspType = problemType;
            try
            {
                tourFile = new StreamReader(fileNameTour);
                while (!tourFile.EndOfStream)
                {
                    curLine = tourFile.ReadLine();
                    if (curLine.Trim() == "")
                    {
                        lineNum++;
                        continue;
                    }
                    keyword = GetKeyword(curLine).ToUpper();

                    if (isEOF == true)
                    {
                        isEOF = false;
                        return tour;
                    }
                    switch (keyword)
                    {
                        case "EOF":
                            return tour;
                        case "-1":
                            return tour;
                        case "NAME":
                            Read_NAME(curLine);
                            tour.Name = name;
                            break;
                        case "TYPE":
                            Read_TYPE(curLine);
                            if (problemType != TSPType.TOUR)
                                throw new InvalidTSPLibValueException("Некорректный тип тура. Должен быть TOUR.");
                            tour.Type = problemType;
                            break;
                        case "COMMENT":
                            Read_COMMENT(curLine);
                            tour.Comment = comment;
                            break;
                        case "DIMENSION":
                            Read_DIMENSION(curLine);
                            tour.Dimension = dimension;
                            break;
                        case "TOUR_SECTION":
                            Read_TOUR_SECTION(tourFile, dimension, ref lineNum, out tour.Tour);
                            break;
                        default:
                            throw new UnknownProblemKeywordException("Неизвестное ключевое слово \"" + keyword + "\".");
                    }
                    lineNum++;
                }
                return tour;
            }
            catch (UnknownProblemKeywordException e)
            {
                // Перевозбуждаем исключение, дополнительно сообщив номер строки.
                throw new UnknownProblemKeywordException("Строка " + lineNum + ":" + Environment.NewLine + e.Message);
            }
            catch (InvalidTSPTourException e)
            {
                // Перевозбуждаем исключение, дополнительно сообщив номер строки.
                throw new InvalidTSPTourException("Строка " + lineNum + ":" + Environment.NewLine + e.Message);
            }

            finally
            {
                if (tourFile != null)
                    tourFile.Close();
                name = tspName;
                comment = tspComment;
                dimension = tspDim; 
                problemType = tspType;
            }
        }

        /// <summary>
        /// Запись текущего тура в файл с именем fileName.
        /// </summary>
        public void WriteTour(string fileName)
        {
            StreamWriter tourFile = null;
            try
            {
                double tourCost = Math.Round(this.Nodes.Cost, 4);

                tourFile = new StreamWriter(fileName);
                tourFile.WriteLine("NAME : " + this.name + "." + tourCost.ToString(CultureInfo.GetCultureInfo("en-GB")) + ".tour");
                tourFile.WriteLine("COMMENT : Найдено с помощью Journey");
                tourFile.WriteLine("COMMENT : Длина = " + tourCost.ToString(CultureInfo.GetCultureInfo("en-GB")));
                tourFile.WriteLine("TYPE : TOUR");
                tourFile.WriteLine("DIMENSION : " + this.Dimension);
                tourFile.WriteLine("TOUR_SECTION");
                Node n = this.Nodes.FirstNode;
                do
                {
                    tourFile.WriteLine(n.Id.ToString());
                    n = n.Succ;
                } while (n != this.Nodes.FirstNode);
                tourFile.WriteLine("-1");
                tourFile.Write("EOF");  
            }
            finally
            {
                if (tourFile != null)
                    tourFile.Close();
            }
        }

        /// <summary>
        /// Возвращает строку со списком значений раздела DEPOT_SECTION.
        /// </summary>
        public string DepotsToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= depots.Count - 1; i++)
            {
                str.Append(depots[i].ToString());
                if (i != depots.Count - 1)
                    str.Append(Environment.NewLine);
            }
            return str.ToString();
        }

        /// <summary>
        /// Возвращает строку со списком спроса раздела DEMAND_SECTION.
        /// </summary>
        public string DemandsToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= demands.Count - 1; i++)
            {
                str.Append("N = " + (i + 1).ToString() + " D: " + demands[i].ToString());
                if (i != demands.Count - 1)
                    str.Append(Environment.NewLine);
            }
            return str.ToString();
        }

        /// <summary>
        /// Возвращает строку со списком значений раздела DISPLAY_DATA_SECTION.
        /// </summary>
        public string DisplayDataToString()
        {
            if (displayData.Count != dimension)
                return "";
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= dimension - 1; i++)
            {
                str.Append((i + 1).ToString() + " " + displayData[i].x.ToString() + " " + displayData[i].y.ToString());
                if (i != displayData.Count - 1)
                    str.Append(Environment.NewLine);
            }
            return str.ToString();
        }

        //==============================================================================
        // Защищённые методы.

        /// <summary>
        /// Ищет ключевое слово в строке и возвращает его, если есть, или пустую строку в ином случае.
        /// </summary>
        private string GetKeyword(string strLine)
        {
            // Шаблон для распознования ключевого слова.
            string strPattern = @"[-]?\b\w+\b";
            bool isMatch = Regex.IsMatch(strLine, strPattern);
            if (isMatch)
            {
                return Regex.Match(strLine, strPattern).ToString();
            }
            else
                return "";
        }

        /// <summary>
        /// Извлекает из строки номер узла и его координаты.
        /// </summary>
        private string GetValue(string strLine)
        {
            string strPattern = @"(?<=:\s*).+(?<=\S*$)";
            bool isMatch = Regex.IsMatch(strLine, strPattern);
            if (isMatch)
            {
                return Regex.Match(strLine, strPattern).ToString().Trim();
            }
            else
                return "";
        }

        /// <summary>
        /// Извлекает из строки номер узла и его координаты.
        /// </summary>
        private void GetCoords(string strLine, out int id, out double x, out double y, out double z)
        {
            string strPattern = @"[^\s\n\r]+";
            x = y = z = 0;
            string strX, strY, strZ;
            MatchCollection coords = Regex.Matches(strLine, strPattern);
            if (!int.TryParse(coords[0].ToString(), out id) || id < 0)
            {
                throw new InvalidTSPLibValueException("Некорректный формат значений номера узла — допускаются целые числа больше нуля.");                        
            }

            if ((coords.Count == 3) && (coordType == TSPCoordType.TWOD_COORDS))
            {
                // Меняем точку или запятую на нужный символ регионального стандарта.
                strX = coords[1].ToString();
                strY = coords[2].ToString();
                FormatRealNumber(ref strX);
                FormatRealNumber(ref strY);

                // Проверяем на соответствие формату.
                if (!double.TryParse(strX, out x) || !(double.TryParse(strY, out y)))
                {
                    throw new InvalidTSPLibValueException("Некорректный формат значений координат — допускаются целые или вещественные числа.");                        
                }
            }
            else
            {
                if ((coords.Count == 4) && (coordType == TSPCoordType.THREED_COORDS))
                {
                    // Меняем точку или запятую на нужный символ регионального стандарта.
                    strX = coords[1].ToString();
                    strY = coords[2].ToString();
                    strZ = coords[3].ToString();
                    FormatRealNumber(ref strX);
                    FormatRealNumber(ref strY);
                    FormatRealNumber(ref strZ);
                    if (!double.TryParse(strX, out x) || !(double.TryParse(strY, out y)) || !(double.TryParse(strZ, out z)))
                    {
                        throw new InvalidTSPLibValueException("Некорректный формат значений координат — допускаются целые или вещественные числа.");
                    }
                }
                else
                {
                    if ((coords.Count == 2) && (coordType == TSPCoordType.TWOD_COORDS))
                    {
                        throw new InvalidTSPLibValueException("Пропущена координата Y узла " + coords[0].ToString() + ".");
                    }
                    if ((coords.Count == 2) && (coordType == TSPCoordType.THREED_COORDS))
                    {
                        throw new InvalidTSPLibValueException("Пропущены координаты Y и Z узла " + coords[0].ToString() + ".");
                    }
                    if ((coords.Count == 3) && (coordType == TSPCoordType.THREED_COORDS))
                    {
                        throw new InvalidTSPLibValueException("Пропущена координата Z узла " + coords[0].ToString() + ".");
                    }
                    else
                    {
                        throw new InvalidTSPLibValueException("Неверное количество координат в описании узла " + coords[0].ToString() + ".");
                    }
                }
            }
        }

        /// <summary>
        /// Извлекает из строки массив строки матрицы расстояний.
        /// </summary>
        private double[] GetCostMatrixRow(string strLine, int elementsInRowNeeded)
        {
            string strPattern = @"[^\s\n\r]+";
            MatchCollection costs = Regex.Matches(strLine, strPattern);
            // Количество элементов в строке должно равняться количеству узлов.
            if (costs.Count != elementsInRowNeeded)
                throw new InvalidTSPLibValueException("Ошибка обработки матрицы — неверное количество элементов.");      
            int i;
            double[] result = new double[dimension];
            string cost;
            for (i = 0; i < elementsInRowNeeded; i++)
            {
                cost = costs[i].ToString();
                FormatRealNumber(ref cost);
                if (!double.TryParse(cost, out result[i]) || (result[i] < 0))
                {
                    throw new InvalidTSPLibValueException("Некорректный формат значений стоимости — допускаются целые или вещественные неотрицательные числа.");                        
                }
            }
            return result;            
        }

        /// <summary>
        /// Создаёт квадратную матрицу с количеством строк и столбцов равной количеству узлов.
        /// </summary>
        private void CreateCostMatrix()
        {
            nodes.CostMatrix = new double[dimension][];
            for (int i = 0; i < dimension; i++)
            {
                nodes.CostMatrix[i] = new double[dimension];
            }

        }

        /// <summary>
        /// Разбирает строку с узлом-депо. Возвращает номер депо.
        /// </summary>
        private void GetDepot(string strLine, out int depot)
        {
            string strPattern = @"[^\s\n\r.]+";
            depot = 0;
            MatchCollection coords = Regex.Matches(strLine, strPattern);
            // Должно быть не более одного числа.
            if (coords.Count > 1)
            {
                throw new InvalidTSPLibValueException("Неверный формат строки при разборе DEPOT_SECTION.");
            }

            // Первое число — номер узла.
            if (!int.TryParse(coords[0].ToString(), out depot) || depot < 0)
            {
                // Выходим, если прочитанное число "-1" — это знак конца списка депо.
                if (depot == -1)
                    return;
                throw new InvalidTSPLibValueException("Некорректный формат значений номера узла — допускаются целые числа больше нуля.");
            }

        }

        /// <summary>
        /// Разбирает строку с требованием. Возвращает два числа — номер узла и его требование.
        /// </summary>
        private void GetDemand(string strLine, out int nodeID, out int nodeDemand)
        {
            string strPattern = @"[^\s\n\r.]+";
            nodeID = 0;
            nodeDemand = -1;
            MatchCollection coords = Regex.Matches(strLine, strPattern);
            // Должно быть не более двух чисел.
            if (coords.Count > 2)
            {
                throw new InvalidTSPLibValueException("Неверный формат строки при разборе DEMAND_SECTION.");
            }

            // Первое число — номер узла.
            if (!int.TryParse(coords[0].ToString(), out nodeID) || nodeID < 0)
            {
                throw new InvalidTSPLibValueException("Некорректный формат значений номера узла — допускаются целые числа больше нуля.");                        
            }

            // Второе — его значение. 
            if (!int.TryParse(coords[1].ToString(), out nodeDemand) || nodeDemand < 0)
            {
                throw new InvalidTSPLibValueException("Некорректный формат значений требования узла — допускаются целые числа больше или равное нулю.");
            }

        }

        /// <summary>
        /// Заменяет все точку или запятую в числе на принятый символ регионального стандарта.
        /// </summary>
        private void FormatRealNumber(ref string num)
        {
            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
            num = num.Replace(',', separator);
            num = num.Replace('.', separator);
        }

        /// <summary>
        /// Читает и обрабатывает верхнетреугольную матрицу. 
        /// </summary>
        private void ReadUpperRowMatrix(string curLine, int curIndex)
        {

            double[] row = GetCostMatrixRow(curLine, dimension - curIndex - 1);
            // Заполняем значение всех строк в матрице стоимости. 
            for (int i = 0; i < dimension - curIndex - 1; i++)
            {
                Nodes.CostMatrix[curIndex][curIndex + i + 1] = row[i];
                Nodes.CostMatrix[curIndex + i + 1][curIndex] = row[i];
            }
        }

        /// <summary>
        /// Читает и обрабатывает нижнетреугольную матрицу. 
        /// </summary>
        private void ReadLowerRowMatrix(string curLine, int curIndex)
        {
            double[] row = GetCostMatrixRow(curLine, curIndex + 1);
            // Заполняем значение всех строк в матрице стоимости. 
            for (int i = 0; i < curIndex + 1; i++)
            {
                Nodes.CostMatrix[curIndex + 1][i] = row[i];
                Nodes.CostMatrix[i][curIndex + 1] = row[i];
            }
        }

        /// <summary>
        /// Проверка прочитанного раздела спецификации на соответствии формату и на отстутствие конфликтов.
        /// </summary>
        private void CheckSpecificationPart()
        {
           // Нужно проверить, все ли узлы из DEPOT секции есть в DEMAND секции. 
           // При этом их требование должно равняться нулю.
        }

        //-------------------------------------------------
        // Раздел спецификации.

        private void Read_NAME(string strLine)
        {
            name = GetValue(strLine);
            if (name == "")
            {
                throw new InvalidTSPLibValueException("NAME: параметр не задан.");
            }
        }

        private void Read_TYPE(string strLine)
        {
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "TSP":
                    problemType = TSPType.TSP;
                    break;
                case "ATSP":
                    problemType = TSPType.ATSP; // Не реализовано.
                    throw new NotImplementedTSPException("Тип \"ATSP\" не реализован в текущей версии.");
                case "SOP":
                    problemType = TSPType.SOP; // Не реализовано.
                    throw new NotImplementedTSPException("Тип \"SOP\" не реализован в текущей версии.");
                case "HCP":
                    problemType = TSPType.HCP; // Не реализовано.
                    throw new NotImplementedTSPException("Тип \"HCP\" не реализован в текущей версии."); ;
                case "CVRP":
                    problemType = TSPType.CVRP;
                    break;
                case "TOUR":
                    problemType = TSPType.TOUR;
                    break;
                default:
                    throw new InvalidTSPLibValueException("TYPE: неизвестный тип (или отсутствует).");
            }
        }

        private void Read_COMMENT(string strLine)
        {
            // Может быть несколько строчек с комментариями, поэтому каждый раз прибавляем новую строчку.
            string curComment = GetValue(strLine);
            // В первый раз просто присваимваем. 
            if (comment == "")
            {
                comment = curComment;
            }
            else
            // В последующие разы делаем перенос строки.
            {
                comment = comment + Environment.NewLine + curComment;
            }
        }

        private void Read_DIMENSION(string strLine)
        {
            strLine = GetValue(strLine);
            if (!int.TryParse(strLine, out dimension))
            {
                throw new InvalidTSPLibValueException("DIMENSION: значение должно быть целым числом.");
            }
            if (dimension < 2) 
            {
                throw new InvalidTSPLibValueException("DIMENSION: значение должно быть целым числом больше или равное двум.");
            }
        }

        private void Read_CAPACITY(string strLine)
        {
            strLine = GetValue(strLine);
            if (!int.TryParse(strLine, out capacity))
            {
                throw new InvalidTSPLibValueException("CAPACITY: значение должно быть целым числом.");
            }
            if (capacity <= 0)
            {
                throw new InvalidTSPLibValueException("CAPACITY: значение должно быть целым числом больше 0.");
            }
        }

        private void Read_EDGE_WEIGHT_TYPE(string strLine)
        {
            if (nodes == null)
                nodes = new NodesList();
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "EXPLICIT":
                    weightType = TSPEdgeWeightType.EXPLICIT;
                    nodes.Distance = Distances.Distance_EXPLICIT;
                    coordType = TSPCoordType.NO_COORDS;
                    break;
                case "CEIL_2D":
                    weightType = TSPEdgeWeightType.CEIL_2D;
                    nodes.Distance = Distances.Distance_CEIL_2D;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "CEIL_3D":
                    weightType = TSPEdgeWeightType.CEIL_3D;
                    nodes.Distance = Distances.Distance_CEIL_3D;
                    coordType = TSPCoordType.THREED_COORDS;
                    break;
                case "EUC_2D":
                    weightType = TSPEdgeWeightType.EUC_2D;
                    nodes.Distance = Distances.Distance_EUC_2D;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "EUC_3D":
                    weightType = TSPEdgeWeightType.EUC_3D;
                    nodes.Distance = Distances.Distance_EUC_3D;
                    coordType = TSPCoordType.THREED_COORDS;
                    break;
                case "MAN_2D":
                    weightType = TSPEdgeWeightType.MAN_2D;
                    nodes.Distance = Distances.Distance_MAN_2D;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "MAN_3D":
                    weightType = TSPEdgeWeightType.MAN_3D;
                    nodes.Distance = Distances.Distance_MAN_3D;
                    coordType = TSPCoordType.THREED_COORDS;
                    break;
                case "MAX_2D":
                    weightType = TSPEdgeWeightType.MAX_2D;
                    nodes.Distance = Distances.Distance_MAX_2D;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "MAX_3D":
                    weightType = TSPEdgeWeightType.MAX_3D;
                    nodes.Distance = Distances.Distance_MAX_3D;
                    coordType = TSPCoordType.THREED_COORDS;
                    break;
                case "ATT":
                    weightType = TSPEdgeWeightType.ATT;
                    nodes.Distance = Distances.Distance_ATT;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "GEO":
                    weightType = TSPEdgeWeightType.GEO;
                    nodes.Distance = Distances.Distance_GEO;
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "XRAY1":
                    throw new NotImplementedTSPException("EDGE_WEIGHT_TYPE: \"XRAY1\" не реализован в текущей версии.");
                case "XRAY2":
                    throw new NotImplementedTSPException("EDGE_WEIGHT_TYPE: \"XRAY1\" не реализован в текущей версии.");
                case "SPECIAL":
                    throw new NotImplementedTSPException("EDGE_WEIGHT_TYPE: \"SPECIAL\" не реализован в текущей версии.");
                default:
                    throw new InvalidTSPLibValueException("EDGE_WEIGHT_TYPE: неизвестный тип (или отсутствует).");
            }
        }

        private void Read_EDGE_WEIGHT_FORMAT(string strLine)
        {
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "FUNCTION":
                    weightFormat = TSPEdgeWeightFormat.FUNCTION;
                    break;
                case "FULL_MATRIX":
                    weightFormat = TSPEdgeWeightFormat.FULL_MATRIX;
                    break;
                case "LOWER_COL":
                    weightFormat = TSPEdgeWeightFormat.LOWER_COL;
                    break;
                case "LOWER_DIAG_COL":
                    weightFormat = TSPEdgeWeightFormat.LOWER_DIAG_COL;
                    break;
                case "LOWER_DIAG_ROW":
                    weightFormat = TSPEdgeWeightFormat.LOWER_DIAG_ROW;
                    break;
                case "LOWER_ROW":
                    weightFormat = TSPEdgeWeightFormat.LOWER_ROW;
                    break;
                case "UPPER_COL":
                    weightFormat = TSPEdgeWeightFormat.UPPER_COL;
                    break;
                case "UPPER_DIAG_COL":
                    weightFormat = TSPEdgeWeightFormat.UPPER_DIAG_COL;
                    break;
                case "UPPER_DIAG_ROW":
                    weightFormat = TSPEdgeWeightFormat.UPPER_DIAG_ROW;
                    break;
                case "UPPER_ROW":
                    weightFormat = TSPEdgeWeightFormat.UPPER_ROW;
                    break;
                default:
                    throw new InvalidTSPLibValueException("EDGE_WEIGHT_FORMAT: неизвестный формат (или отсутствует).");
            }
        }

        private void Read_EDGE_DATA_FORMAT(string strLine)
        {
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "EDGE_LIST":
                    edgeDataFormat = TSPEdgeDataFormat.EDGE_LIST;
                    break;
                case "ADJ_LIST":
                    edgeDataFormat = TSPEdgeDataFormat.ADJ_LIST;
                    break;
                default:
                    throw new InvalidTSPLibValueException("EDGE_DATA_FORMAT: неизвестный тип (или отсутствует).");
            }
        }

        private void Read_NODE_COORD_TYPE(string strLine)
        {
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "NO_COORDS":
                    coordType = TSPCoordType.NO_COORDS;
                    break;
                case "TWOD_COORDS":
                    coordType = TSPCoordType.TWOD_COORDS;
                    break;
                case "THREED_COORDS":
                    coordType = TSPCoordType.THREED_COORDS;
                    break;
                default:
                    throw new InvalidTSPLibValueException("NODE_COORD_TYPE: неизвестный тип (или отсутствует).");
            }
        }

        private void Read_DISPLAY_DATA_TYPE(string strLine)
        {
            strLine = GetValue(strLine).ToUpper();
            switch (strLine)
            {
                case "COORD_DISPLAY":
                    displayDataType = TSPDisplayDataType.COORD_DISPLAY;
                    break;
                case "TWOD_DISPLAY":
                    displayDataType = TSPDisplayDataType.TWOD_DISPLAY;
                    break;
                case "NO_DISPLAY":
                    displayDataType = TSPDisplayDataType.NO_DISPLAY;
                    break;
                default:
                    throw new InvalidTSPLibValueException("DISPLAY_DATA_TYPE: неизвестный тип (или отсутствует).");
            }
        }

        //-------------------------------------------------
        // Раздел данных.

        private void Read_NODE_COORD_SECTION(StreamReader problemFile, ref int lineNum)
        {
            displayDataType = TSPDisplayDataType.COORD_DISPLAY;

            if (coordType != TSPCoordType.TWOD_COORDS && coordType != TSPCoordType.THREED_COORDS)
                throw new InvalidTSPLibValueException("NODE_COORD_SECTION конфликтует с NODE_COORD_TYPE: " + coordType.ToString() + ".");

            // Создаём список узлов, если ранее не был создан. 
            if (nodes == null)
                nodes = new NodesList();

            // Выделяем память под узлы.
            nodes.CreateNodes(dimension);

            // Объеявляем переменные.
            string curLine = "";
            int nodeID;
            double x, y, z;

            // Строка за строкой разбираем координаты.
            for (int i = 0; i < dimension; i++)
            {
                // Проверяем случай, когда файл закончился ранее объявленного количества узлов (dimension).
                if (problemFile.EndOfStream == true)
                {
                    throw new InvalidTSPLibValueException("NODE_COORD_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                }

                // Читаем строку.
                curLine = problemFile.ReadLine().ToUpper();
                lineNum++;
                if (curLine.Trim() == "")
                {
                    i--;
                    continue;
                }

                // Выходим, если прочитано ключевое слово окончания разбора.
                if (curLine == "EOF")
                {
                    if (i != dimension)
                        throw new InvalidTSPLibValueException("NODE_COORD_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                    isEOF = true;
                    return;
                }

                // Разбираем координаты.
                GetCoords(curLine, out nodeID, out x, out y, out z);

                // Случай, если сбит порядок узлов.
                if (nodeID != i + 1)
                {
                    throw new InvalidTSPLibValueException("Сбит порядок узлов в NODE_COORD_SECTION.");
                }

                // Записываем их в переменную.
                nodes.ElementAt(i).Id = nodeID;
                nodes.ElementAt(i).Coords.X = x;
                nodes.ElementAt(i).Coords.Y = y;
                nodes.ElementAt(i).Coords.Z = z;
            }
        }

        private void Read_DEPOT_SECTION(StreamReader problemFile, ref int lineNum)
        {
            string curLine = "";
            int depot = 0;

            // Строка за строкой разбираем координаты.
            while (depot != -1)
            {
                // Выходим, если встречен конец файла.
                if (problemFile.EndOfStream == true)
                {
                    return;
                }

                // Читаем строку.
                curLine = problemFile.ReadLine().ToUpper();
                lineNum++;
                if (curLine.Trim() == "")
                {
                    continue;
                }


                // Выходим, если прочитано ключевое слово окончания разбора.
                if (curLine == "EOF")
                {
                    isEOF = true;
                    return;
                }

                // Разбираем номер депо.
                GetDepot(curLine, out depot);

                // Выходим, если встретили номер узла равным -1.
                if (depot == -1)
                    break;

                // Проверяем, не указан ли неверный номер. 
                if (depot > dimension)
                    throw new InvalidTSPLibValueException("DEPOT_SECTION: указан несуществующий узел.");

                // Проверяем, нет ли в списке уже такого номера.
                if (depots.Contains(depot))
                    throw new InvalidTSPLibValueException("DEPOT_SECTION: повтор номера узла " + depot.ToString() + ".");

                // Добавляем номер депо в список.
                depots.Add(depot);
            }
        }

        private void Read_DEMAND_SECTION(StreamReader problemFile, ref int lineNum)
        {
            string curLine = "";
            int nodeID = 0; 
            int nodeDemand = -1;

            // Строка за строкой разбираем требования.
            for (int i = 0; i < dimension; i++)
            {
                // Проверяем случай, когда файл закончился ранее объявленного количества узлов (dimension).
                if (problemFile.EndOfStream == true)
                {
                    throw new InvalidTSPLibValueException("DEMAND_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                }

                // Читаем строку.
                curLine = problemFile.ReadLine().ToUpper();
                lineNum++;
                if (curLine.Trim() == "")
                {
                    i--;
                    continue;
                }

                // Выходим, если прочитано ключевое слово окончания разбора.
                if (curLine == "EOF")
                {
                    if (i != dimension)
                        throw new InvalidTSPLibValueException("DEMAND_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                    isEOF = true;
                    return;
                }

                // Разбираем требование.
                GetDemand(curLine, out nodeID, out nodeDemand);

                // Проводим проверки.

                // Случай, если сбит порядок узлов.
                if (nodeID != i + 1)
                {
                    throw new InvalidTSPLibValueException("Сбит порядок узлов в DEMAND_SECTION.");
                }

                // Добавляем требование в список.
                demands.Add(nodeDemand);
            }
        }

        private void Read_EDGE_DATA_SECTION()
        {

        }

        private void Read_FIXED_EDGES_SECTION()
        {
        }

        private void Read_DISPLAY_DATA_SECTION(StreamReader problemFile, ref int lineNum)
        {
            displayDataType = TSPDisplayDataType.TWOD_DISPLAY;
            TSPCoordType oldCoordType = coordType;
            coordType = TSPCoordType.TWOD_COORDS; // Временно включаем "режим" «две координаты».
         
            // Объеявляем переменные.
            string curLine = "";
            int nodeID;
            double x, y, z;

            try
            {
                // Строка за строкой разбираем координаты.
                for (int i = 0; i < dimension; i++)
                {
                    // Проверяем случай, когда файл закончился ранее объявленного количества узлов (dimension).
                    if (problemFile.EndOfStream == true)
                    {
                        throw new InvalidTSPLibValueException("DISPLAY_DATA_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                    }

                    // Читаем строку.
                    curLine = problemFile.ReadLine().ToUpper();
                    lineNum++;
                    if (curLine.Trim() == "")
                    {
                        i--;
                        continue;
                    }

                    // Выходим, если прочитано ключевое слово окончания разбора.
                    if (curLine == "EOF")
                    {
                        if (i != dimension)
                            throw new InvalidTSPLibValueException("DISPLAY_DATA_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                        isEOF = true;
                        return;
                    }

                    // Разбираем координаты.
                    GetCoords(curLine, out nodeID, out x, out y, out z);

                    // Случай, если сбит порядок узлов.
                    if (nodeID != i + 1)
                    {
                        throw new InvalidTSPLibValueException("Сбит порядок узлов в DISPLAY_DATA_SECTION.");
                    }

                    // Добавляем координаты в массив.
                    displayData.Add(new DisplayDataEl(x, y));
                }
            }
            finally
            {
                coordType = oldCoordType; // Возвращаем координатный режим обратно.
            }
        }

        private void Read_TOUR_SECTION(StreamReader tourFile, int dimension, ref int lineNum, out int[] tour)
        {
            // В массиве храним номера (идентификаторов) узлов в том порядке, в каком они будут читаться из файла.
            tour = new int[dimension];

            // Объеявляем переменные.
            string curLine = "";
            uint nodeID;

            // Строка за строкой разбираем координаты.
            for (int i = 0; i < dimension; i++)
            {
                // Проверяем случай, когда файл закончился ранее объявленного количества узлов (dimension).
                if (tourFile.EndOfStream == true)
                {
                    throw new InvalidTSPTourException("TOUR_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                }

                // Читаем строку.
                curLine = tourFile.ReadLine().ToUpper();
                lineNum++;
                if (curLine.Trim() == "")
                {
                    i--;
                    continue;
                }

                // Выходим, если прочитано ключевое слово окончания разбора.
                if ((curLine == "EOF") || (curLine == "-1"))
                {
                    if (i != dimension)
                        throw new InvalidTSPTourException("TOUR_SECTION: указано недостаточное количество узлов (необходимо " + dimension.ToString() + ").");
                    isEOF = true;
                    return;
                }

                if (!UInt32.TryParse(curLine, out nodeID))
                {
                    throw new InvalidTSPTourException("TOUR_SECTION: некорректное значение номера узла.");
                }

                // Случай, если узел уже в массиве.
                if (tour.Contains(Convert.ToInt32(nodeID)))
                {
                    throw new InvalidTSPTourException("TOUR_SECTION: Повторный номер узла " + nodeID.ToString() + ".");
                }

                // Если всё ОК, то пишем текущий прочитанный узел в массив.
                tour[i] = Convert.ToInt32(nodeID);
            }
        }

        private void Read_EDGE_WEIGHT_SECTION(StreamReader problemFile, ref int lineNum)
        {
            // Создаём список узлов, если ранее не был создан. 
            if (nodes == null)
                nodes = new NodesList();

            nodes.CreateNodes(dimension);

            nodes.CostMatrix = new double[dimension][];

            int i;
            string curLine = "";
            int rowsCount;

            switch (weightFormat)
            {
                case TSPEdgeWeightFormat.FULL_MATRIX:
                    rowsCount = dimension;
                    break;
                case TSPEdgeWeightFormat.UPPER_ROW:
                    CreateCostMatrix();
                    rowsCount = dimension - 1;
                    break;
                case TSPEdgeWeightFormat.LOWER_ROW:
                    CreateCostMatrix();
                    rowsCount = dimension - 1;
                    break;
                case TSPEdgeWeightFormat.UPPER_DIAG_ROW:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_DIAG_ROW\" не реализован в текущей версии.");
                case TSPEdgeWeightFormat.LOWER_DIAG_ROW:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_DIAG_ROW\" не реализован в текущей версии.");
                case TSPEdgeWeightFormat.UPPER_COL:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_COL\" не реализован в текущей версии.");
                case TSPEdgeWeightFormat.LOWER_COL:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_COL\" не реализован в текущей версии.");
                case TSPEdgeWeightFormat.UPPER_DIAG_COL:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_DIAG_COL\" не реализован в текущей версии.");
                case TSPEdgeWeightFormat.LOWER_DIAG_COL:
                    throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_DIAG_COL\" не реализован в текущей версии.");
                default:
                    throw new InvalidTSPLibValueException("EDGE_WEIGHT_TYPE: неизвестный тип (или отсутствует).");
            }

            // Строка за строкой разбираем координаты.
            for (i = 0; i < rowsCount; i++)
            {
                // Проверяем случай, когда файл закончился ранее объявленного количества узлов (dimension).
                if (problemFile.EndOfStream == true)
                {
                    throw new InvalidTSPLibValueException("EDGE_WEIGHT_SECTION: указано недостаточное количество строк матрицы (необходимо " + dimension.ToString() + ").");
                }

                curLine = problemFile.ReadLine();
                lineNum++;
                if (curLine.Trim() == "")
                {
                    i--;
                    continue;
                }

                // Выходим, если прочитано ключевое слово окончания разбора.
                if (curLine == "EOF")
                {
                    if (i != dimension)
                        throw new InvalidTSPLibValueException("EDGE_WEIGHT_SECTION: указано недостаточное количество строк матрицы (необходимо " + dimension.ToString() + ").");
                    isEOF = true;
                    return;
                }

                switch (weightFormat)
                {
                    case TSPEdgeWeightFormat.FULL_MATRIX:
                        double[] costRow = GetCostMatrixRow(curLine, dimension);

                        nodes.CostMatrix[i] = costRow;

                        if (nodes.CostMatrix[i][i] != 0)
                            throw new InvalidTSPLibValueException("EDGE_WEIGHT_SECTION: неверный формат матрицы. Текущий узел должен быть помечен как \"0\".");
                        break;
                    case TSPEdgeWeightFormat.UPPER_ROW:
                        ReadUpperRowMatrix(curLine, i);
                        break;
                    case TSPEdgeWeightFormat.LOWER_ROW:
                        ReadLowerRowMatrix(curLine, i);
                        break;
                    case TSPEdgeWeightFormat.UPPER_DIAG_ROW:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_DIAG_ROW\" не реализован в текущей версии.");
                    case TSPEdgeWeightFormat.LOWER_DIAG_ROW:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_DIAG_ROW\" не реализован в текущей версии.");
                    case TSPEdgeWeightFormat.UPPER_COL:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_COL\" не реализован в текущей версии.");
                    case TSPEdgeWeightFormat.LOWER_COL:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_COL\" не реализован в текущей версии.");
                    case TSPEdgeWeightFormat.UPPER_DIAG_COL:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"UPPER_DIAG_COL\" не реализован в текущей версии.");
                    case TSPEdgeWeightFormat.LOWER_DIAG_COL:
                        throw new NotImplementedTSPException("EDGE_WEIGHT_SECTION: \"LOWER_DIAG_COL\" не реализован в текущей версии.");
                    default:
                        throw new InvalidTSPLibValueException("EDGE_WEIGHT_TYPE: неизвестный тип (или отсутствует).");
                }
            }
        }

    }
}