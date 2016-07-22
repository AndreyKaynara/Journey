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
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;

using JourneyExceptions;
using JourneyIO;
using JourneyLogs;
using JourneyTSP;
using JourneyToursComputing;

namespace JourneyGUI
{
    public partial class MainForm : Form
    {
        //==============================================================================
        // Работа с таблицами параметров проекта и задачи.

        private void LoadParametersToGridView()
        {
            string strRandomFirstNode;
            if (JourneyController.Project.RandomFirstNode)
                strRandomFirstNode = "yes";
            else
                strRandomFirstNode = "no";
            ParametersGV.Rows.Clear();
            ParametersGV.Rows.Add(new object[] { ParametersNames.ProblemFile, JourneyController.Project.ProblemFileName });
            ParametersGV.Rows.Add(new object[] { ParametersNames.TourFile, JourneyController.Project.TourFileName });
            ParametersGV.Rows.Add(new object[] { ParametersNames.LogLevel, JourneyController.Project.LogLevel.ToString() });
            ParametersGV.Rows.Add(new object[] { ParametersNames.Optimum, JourneyController.Project.Optimum.ToString() });
            ParametersGV.Rows.Add(new object[] { ParametersNames.RandomFirstNode, strRandomFirstNode });
            ParametersGV.Rows.Add(new object[] { ParametersNames.LKNearestNeighbors, JourneyController.Project.LKNearestNeighbors.ToString() });
            ParametersGV.Rows.Add(new object[] { ParametersNames.LKGap, JourneyController.Project.LKGap.ToString() });
        }

        private void LoadProblemToGridView()
        {
            ProblemGV.Rows.Clear();
            ProblemGV.Rows.Add(new object[] { "NAME", JourneyController.TSP.Name });
            ProblemGV.Rows.Add(new object[] { "COMMENT", JourneyController.TSP.Comment });
            ProblemGV.Rows.Add(new object[] { "DIMENSION", JourneyController.TSP.Dimension.ToString() });
            ProblemGV.Rows.Add(new object[] { "TYPE", JourneyController.TSP.ProblemType.ToString() });
            ProblemGV.Rows.Add(new object[] { "EDGE_WEIGHT_TYPE", JourneyController.TSP.WeightType.ToString() });
            ProblemGV.Rows.Add(new object[] { "COORD_TYPE", JourneyController.TSP.CoordType.ToString() });
        }


        //==============================================================================
        // Работа с отображением тура на MapControl.

        /* Полностью рисует график текущего тура. */
        private void DrawGraph(bool drawPath = true)
        {
            try
            {
                try
                {
                    if (JourneyController.TSP.DisplayDataType == TSPDisplayDataType.NO_DISPLAY)
                    {
                        return;
                    }
                    SetMapRanges();
                    DrawMap();
                    if (drawPath)
                      DrawPath();
                }
                catch (Exception e)
                {
                    throw new DrawMapException("Ошибка при построении тура!\r\n" + e.Message);
                }
            }
            catch (Exception e)
            {
                ErrorHandle.DoHandle(e);
            }


        }

        /* Рассчитывает границы карты согласно текущим максимальным и минимальным координатам тура. */ 
        private void SetMapRanges()
        {
            if (JourneyController.TSP.DisplayData.Count == JourneyController.TSP.Nodes.Dimension)
            {
                double maxX = 0;
                for (int i = 0; i < JourneyController.TSP.DisplayData.Count; i++)
                {
                    if (JourneyController.TSP.DisplayData[i].x > maxX)
                        maxX = JourneyController.TSP.DisplayData[i].x;
                }
                double minX = double.MaxValue;
                for (int i = 0; i < JourneyController.TSP.DisplayData.Count; i++)
                {
                    if (JourneyController.TSP.DisplayData[i].x < minX)
                        minX = JourneyController.TSP.DisplayData[i].x;
                }
                double maxY = 0;
                for (int i = 0; i < JourneyController.TSP.DisplayData.Count; i++)
                {
                    if (JourneyController.TSP.DisplayData[i].y > maxY)
                        maxY = JourneyController.TSP.DisplayData[i].y;
                }
                double minY = double.MaxValue;
                for (int i = 0; i < JourneyController.TSP.DisplayData.Count; i++)
                {
                    if (JourneyController.TSP.DisplayData[i].y < minY)
                        minY = JourneyController.TSP.DisplayData[i].y;
                }
                MapControl.RangeX = new IntRange((int)minX - 5, (int)maxX + 5);
                MapControl.RangeY = new IntRange((int)minY - 5, (int)maxY + 5);
            }
            else
            {
                MapControl.RangeX = new IntRange((int)JourneyController.TSP.Nodes.GetMinCoordX() - 5, (int)JourneyController.TSP.Nodes.GetMaxCoordX() + 5);
                MapControl.RangeY = new IntRange((int)JourneyController.TSP.Nodes.GetMinCoordY() - 5, (int)JourneyController.TSP.Nodes.GetMaxCoordY() + 5);
            }
        }

        /* Рисует карту узлов на MapControl. */
        private void DrawMap()
        {
            // Обнуляем текущую карту узлов.
            MapControl.Map = null;

            // Создаём двумерный массив координат узлов.
            int[,] map = new int[JourneyController.TSP.Nodes.Dimension, 2];

            bool ddCoords = false;

            // Если заданы координаты для отоброжения в секции DISPLAY_DATA, то выводим координаты
            // по ним. Иначе по координатам в узлах.
            if (JourneyController.TSP.DisplayData.Count == JourneyController.TSP.Nodes.Dimension)
            {
                ddCoords = true;
            }

            Node n = null;
            // Заполняем его координатами X и Y текущего тура. 
            for (int i = 0; i < JourneyController.TSP.Nodes.Dimension; i++)
            {
                if (!ddCoords)
                {
                    n = JourneyController.TSP.Nodes.ElementWithId(i + 1);
                    map[i, 0] = (int)n.Coords.X;
                    map[i, 1] = (int)n.Coords.Y;
                }
                else
                {
                    map[i, 0] = (int)JourneyController.TSP.DisplayData[i].x;
                    map[i, 1] = (int)JourneyController.TSP.DisplayData[i].y;
                }
            }
            
            // Присваиваем текущую карту MapControl'у.
            MapControl.Map = map;
        }

        /* Рисует текущий тур на MapControl. */
        private void DrawPath()
        {
            // Обнуляем текущий тур. 
            MapControl.Path = null;
            int[] path = new int[JourneyController.TSP.Nodes.Dimension + 1];
            // Сохраняем в массив текущий список узлов (тур).
            JourneyController.TSP.Nodes.SaveTourToArray(ref path);

            // Номера узлов изначально в массиве начинаются с 1. Но в MapControl идёт нумерация с нуля.
            // Поэтому уменьшаем имя (индекс) каждого города на 1. 
            for (int i = 0; i < path.Count() - 1; i++)
            {  
                path[i] = path[i] - 1;
            }
            // Конечный элемент - снова первый (возвращаемся в изначальный город). 
            path[path.Count() - 1] = path[0];

            // Назначаем текущий путь MapControl'у.
            MapControl.Path = path;
        }

        //==============================================================================

        /* Загружает проект и указанную в нём задачу. */
        private void OpenProject(string projectName)
        {
            try
            {
                // Пробуем загрузить проект.
                JourneyController.Initialize(projectName);

                if (JourneyController.TSP.Dimension < 4)
                    throw new InvalidTSPLibValueException("Для работы необходима задача с минимум 4 городами.");

                // Загружаем информацие о проекте и задаче в таблицы.
                LoadParametersToGridView();
                LoadProblemToGridView();
                // Обновляем заголовок программы и состояние элементов программы. 
                UpdateCaption();
                UpdateState(true);
                // Рисует узлы на графике.
                DrawGraph(false);

                // Если был передан путь для быстрого сохранения/загрузки тура, делаем соответствующие пункты меню активными.
                if (JourneyController.Project.TourFileName != "")
                {
                    mmiLoadTour.Enabled = true;
                    mmiSaveTour.Enabled = true;
                }
                else
                {
                    mmiLoadTour.Enabled = false;
                    mmiSaveTour.Enabled = false;
                }

                Logs.LogLevel = JourneyController.Project.LogLevel;

                // Пишем в лог информацию о загруженной задаче. 
                Logs.Notify("Загружен проект " + Path.GetFullPath(projectName), 0);
             
            }
            catch (Exception e)
            {
                CloseProject(false);
                ErrorHandle.DoHandle(e);
            }
        }

        /* Сохраняет текущий проект. */
        private void SaveProject(string projectName)
        {
            if (JourneyController.Instance.IsInitialized)
            {
                JourneyController.Project.SaveProject(projectName);
                UpdateCaption();
            }
        }

        /* Закрывает текущий проект и возвращает состояние программы к моменту первого запуска. */
        private void CloseProject(bool clearLogs)
        {
            JourneyController.Instance.Close();
            ParametersGV.Rows.Clear();
            ProblemGV.Rows.Clear();
            UpdateCaption();
            UpdateState(false);
            MapControl.Map = null;
            MapControl.Path = null;
            if (clearLogs)
                tbLogs.Clear();
        }

        /* Загружает тур в проект.  */
        private void LoadTour(string tourFileName)
        {
            try
            {
                // Если указан относительный путь тура, то загружаем тур относительно файла проекта.
                if (!Path.IsPathRooted(tourFileName))
                    tourFileName = Path.GetDirectoryName(JourneyController.Project.ProjectFileName) + "/" + tourFileName;
                TSPTour tour = JourneyController.TSP.ReadTour(tourFileName);
                if (tour.Dimension != JourneyController.TSP.Dimension)
                    throw new InvalidTSPTourException("Количество узлов в загружаемом туре не совпадает с количеством узлов в загруженном туре.");
                JourneyController.TSP.Nodes.CreateTourFromArray(tour.Tour);
                Logs.Notify("Тур  " + Path.GetFullPath(tourFileName) + " успешно загружен.", 0);
                Logs.Notify("NAME:" + tour.Name, 2);
                Logs.Notify("COMMENT: " + tour.Comment, 2);
                DrawGraph();
                LogGraphInfo();
                double cost = JourneyController.TSP.Nodes.Cost;
                if (cost < JourneyController.TSP.Nodes.BestCost)
                    JourneyController.TSP.Nodes.BestCost = cost;

            }
            catch (Exception e)
            {
                ErrorHandle.DoHandle(e);
            }
        }

        /* Сохраняет текущий тур в файл. */
        private void SaveTour(string tourFileName)
        {
            try
            {
                // Если указан относительный путь тура, то сохраняем тур относительно файла проекта.
                if (!Path.IsPathRooted(tourFileName))
                    tourFileName = Path.GetDirectoryName(JourneyController.Project.ProjectFileName) + "/" + tourFileName;
                JourneyController.TSP.WriteTour(tourFileName);
                Logs.Notify("Тур успешно сохранён по пути " + Path.GetFullPath(tourFileName), 0);
            }
            catch (Exception e)
            {
                ErrorHandle.DoHandle(e);
            }
        }

        //==============================================================================

        /// <summary>
        /// Возвращает значение отклонения длины тура в зависимости от оптимума. Если параметр tourCost = 0 или отрицателен, то будет подсчитано отклонение от длины текущего тура, в ином случае — от переданной длины.
        /// </summary>
        private double GetDeviation(double tourCost = 0)
        {
            if ((JourneyController.Project.Optimum <= 0) || (JourneyController.Project.Optimum == Double.PositiveInfinity))
                return 0;
            if (tourCost <= 0)
                tourCost = JourneyController.TSP.Nodes.Cost;
            return (JourneyController.Project.Optimum * 100) / tourCost;
        }

        /// <summary>
        /// Запускает тестирование алгоритма Лина-Кернигана, пробуя в качестве изначального различные туры.
        /// </summary>
        private void DoTesting(int runs = 10)
        {
            if (runs <= 0)
                runs = 1;
            NodesList nodes = JourneyController.TSP.Nodes;
            double bestCost = double.MaxValue;
            double worseCost = 0;
            double[] costs = new double[runs];
            double bestTime = double.MaxValue;
            double worseTime = 0;
            double[] times = new double[runs];

            Logs.AddBigSeparator();
            Logs.Notify("Начало тестирования.", 0);
            Logs.Notify("Имя задачи: " + JourneyController.TSP.Name, 0);
            Logs.AddSeparator();
            for (int n = 1; n <= 3; n++)
            {
                if (n == 1)
                    Logs.Notify("Тестирование алгоритма Лина-Кернигана на основе случайного тура", 0);
                if (n == 2)
                    Logs.Notify("Тестирование алгоритма Лина-Кернигана на основе тура ближайшего соседа", 0);
                if (n == 3)
                    Logs.Notify("Тестирование алгоритма Лина-Кернигана на основе 2-опт тура", 0);

                // Устанавливаем значения по умолчанию для статистических переменных.
                bestCost = double.MaxValue;
                worseCost = 0;
                costs = new double[runs];
                bestTime = double.MaxValue;
                worseTime = 0;
                times = new double[runs];

                for (int i = 1; i <= runs; i++)
                {
                    if (n == 1)
                    {
                        ToursComputing.RandomTour(JourneyController.TSP.Nodes, JourneyController.Project.RandomFirstNode);
                    }
                    if (n == 2)
                    {
                        ToursComputing.NearestNeighbor(JourneyController.TSP.Nodes, JourneyController.Project.RandomFirstNode);
                    }
                    if (n == 3)
                    {
                        ToursComputing.RandomTour(JourneyController.TSP.Nodes, JourneyController.Project.RandomFirstNode);
                        ToursComputing.TwoOpt(JourneyController.TSP.Nodes);
                    }
                    DateTime preTime = new DateTime();
                    DateTime afterTime = new DateTime();
                    preTime = DateTime.Now;
                    double res = ToursComputing.LinKernighan(nodes, JourneyController.Project.LKNearestNeighbors, JourneyController.Project.LKGap);
                    afterTime = DateTime.Now;
                    Logs.Notify("  Результат: " + res, 0);
                    if (res > worseCost)
                        worseCost = res;
                    if (res < bestCost)
                        bestCost = res;
                    costs[i - 1] = res;
                    double seconds = (afterTime - preTime).TotalSeconds;
                    if (seconds > worseTime)
                        worseTime = seconds;
                    if (seconds < bestTime)
                        bestTime = seconds;
                    times[i - 1] = seconds;
                }
                Logs.Notify(runs + " запусков успешно завершены.", 0);
                Logs.Notify("Лучший результат: " + bestCost + " Точность: " + Math.Round(GetDeviation(bestCost), 2) + "%", 0);
                Logs.Notify("Средний результат: " + costs.Sum() / runs + " Точность: " + Math.Round(GetDeviation(costs.Sum() / runs), 2) + "%", 0);
                Logs.Notify("Худший результат: " + worseCost + " Точность: " + Math.Round(GetDeviation(worseCost), 2) + "%", 0);
                Logs.Notify("Лучшее время: " + Math.Round(bestTime , 2), 0);
                Logs.Notify("Среднее время: " + Math.Round(times.Sum() / runs, 2), 0);
                Logs.Notify("Худшее время: " + Math.Round(worseTime, 2), 0);
                Logs.AddSeparator();
            }
            Logs.Notify("Тестирование завершено.");
            Logs.AddBigSeparator();
        }

        //==============================================================================
        // Вывод различной информации в поле логов.

        /// <summary>
        /// Вывод в логи информации о графе.
        /// </summary>
        private void LogGraphInfo()
        {
            Logs.Notify("  Тур содержит " + JourneyController.TSP.Nodes.Dimension + " рёбер длиной " + JourneyController.TSP.Nodes.Cost + ".", 0);
        }

        /// <summary>
        /// Вывести в логи процент отклоенения от оптимума.
        /// </summary>
        private void LogOptimumDeviation()
        {
            double deviation = GetDeviation();
            if (deviation == 0)
                return;
            Logs.Notify("  Точность: " + Math.Round(deviation, 2) + "%", 1);
        }

        //==============================================================================

        /* Обновляет заголовок программы. */
        private void UpdateCaption()
        {
            if (JourneyController.Instance.IsInitialized)
            {
                this.Text = "Journey" + " — " + Path.GetFileName(JourneyController.Instance.ProjectName);
            }
            else
            {
                this.Text = "Journey";
            }
        }

        /* Обновляет состояние элементов, делая их активными или неактивными.*/
        private void UpdateState(bool active)
        {
            mmiSave.Enabled = active;
            mmiSaveAs.Enabled = active;
            mmiClose.Enabled = active;
            mmiTSP.Enabled = active;
            mmiTour.Enabled = active;
            // Пукнт "тур".
            mmiSaveTour.Enabled = active;
            mmiSaveTourAs.Enabled = active;
            mmiLoadTour.Enabled = active;
            mmiLoadTourAs.Enabled = active;

            // Пункт "решение".
            mmiSolution.Enabled = active;
            mmiRandom.Enabled = active;
            mmiNearestNeigbor.Enabled = active;
            mmi2Opt.Enabled = active;
            mmiLinKernighan.Enabled = active;
            mmiShowBestDistance.Enabled = active;
        }

        //==============================================================================
        // Элементы главого меню. 

        private void miOpen_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CloseProject(false);
                OpenProject(OpenFileDialog.FileName);
            }

        }

        private void mmiSave_Click(object sender, EventArgs e)
        {
            JourneyController.Project.SaveProject(JourneyController.Instance.ProjectName);
        }

        private void mmiSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(JourneyController.Project.ProjectFileName);
            if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                JourneyController.Project.SaveProject(SaveFileDialog.FileName);
                OpenProject(SaveFileDialog.FileName);
            }
        }


        private void mmiClose_Click(object sender, EventArgs e)
        {
            CloseProject(true);
        }

        private void mmiClearLogs_Click(object sender, EventArgs e)
        {
            tbLogs.Clear();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mmiShowDemands_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.DemandsToString());
        }

        private void mmiShowDepots_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.DepotsToString());
        }

        private void mmiShowDisplayCoords_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.DisplayDataToString());
        }

        private void mmiShowCostMatrix_Click(object sender, EventArgs e)
        {
            if (JourneyController.TSP.Nodes.Dimension <= 100)
            {
                MessageBox.Show(JourneyController.TSP.Nodes.CostMatrixToString());
                Logs.Notify(JourneyController.TSP.Nodes.CostMatrixToString(), 3);
            }
            else
            {
                MessageBox.Show("Вывод матрицы стоимосте возможен только для числа узлов <= 100");
            }

        }

        private void mmiTourLength_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Длина тура = " + JourneyController.TSP.Nodes.Cost.ToString());
        }

        private void mmiShowNodes_Click(object sender, EventArgs e)
        {
            Logs.Notify(JourneyController.TSP.Nodes.ToString(), 0);
        }

        private void mmiShowTour_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.Nodes.TourToString());
        }
        private void mmiNodesCount_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.Nodes.Dimension.ToString());
        }

        private void mmiCostMatrix_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.Nodes.CostMatrixToString());
        }

        private void mmiSaveTour_Click(object sender, EventArgs e)
        {
            SaveTour(JourneyController.Project.TourFileName);
        }

        private void mmiLoadTour_Click(object sender, EventArgs e)
        {
            LoadTour(JourneyController.Project.TourFileName);
        }

        private void mmiSaveTourAs_Click(object sender, EventArgs e)
        {
            SaveTourDialog.FileName = JourneyController.TSP.Name + "." + JourneyController.TSP.Nodes.Cost.ToString(CultureInfo.GetCultureInfo("en-GB"));
            if (SaveTourDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveTour(SaveTourDialog.FileName);
            }
        }

        private void mmiLoadTourAs_Click(object sender, EventArgs e)
        {
            if (OpenTourDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadTour(OpenTourDialog.FileName);
            }
        }

        //========
        // Вычисление туров.

        private void mmiRandom_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Logs.Notify("Построение случайного тура...", 0);
                    DateTime preTime = new DateTime();
                    DateTime afterTime = new DateTime();
                    preTime = DateTime.Now;
                    ToursComputing.RandomTour(JourneyController.TSP.Nodes, JourneyController.Project.RandomFirstNode);
                    afterTime = DateTime.Now;
                    Logs.Notify((afterTime - preTime).ToString(@"hh\:mm\:ss\:fff"), 0);
                    DrawGraph();
                    LogGraphInfo();
                    LogOptimumDeviation();
                }
                catch (Exception exception)
                {
                    throw new RandomTourErrorException("Ошибка при построении случайного тура:\r\n" + exception.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                ErrorHandle.DoHandle(exception);
            }
          }

        private void mmiNearestNeigbor_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Logs.Notify("Вычисление тура алгоритмом ближайшего соседа...", 0);
                    DateTime preTime = new DateTime();
                    DateTime afterTime = new DateTime();
                    preTime = DateTime.Now;
                    ToursComputing.NearestNeighbor(JourneyController.TSP.Nodes, JourneyController.Project.RandomFirstNode);
                    afterTime = DateTime.Now;
                    Logs.Notify((afterTime - preTime).ToString(@"hh\:mm\:ss\:fff"), 0);
                    DrawGraph();
                    LogGraphInfo();
                    LogOptimumDeviation();
                }
                catch (Exception exception)
                {
                    throw new NearestNeighborErrorException("Ошибка при вычислении тура алгоритмом ближайшего соседа:\r\n" + exception.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                ErrorHandle.DoHandle(exception);
            }
         }

        private void mmi2Opt_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Logs.Notify("Вычисление тура 2-опт алгоритмом...", 0);
                    DateTime preTime = new DateTime();
                    DateTime afterTime = new DateTime();
                    preTime = DateTime.Now;
                    ToursComputing.TwoOpt(JourneyController.TSP.Nodes);
                    afterTime = DateTime.Now;
                    Logs.Notify((afterTime - preTime).ToString(@"hh\:mm\:ss\:fff"), 0);
                    DrawGraph();
                    LogGraphInfo();
                    LogOptimumDeviation();
                }
                catch (Exception exception)
                {
                    throw new TwoOptErrorException("Ошибка при вычислении тура алгоритмом 2-опт алгоритмом:\r\n" + exception.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                ErrorHandle.DoHandle(exception);
            }
}

        private void mmiLinKernighan_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    Logs.Notify("Вычисление тура алгоритмом Лина-Кернигана...", 0);
                    NodesList nodes = JourneyController.TSP.Nodes;
                    DateTime preTime = new DateTime();
                    DateTime afterTime = new DateTime();
                    preTime = DateTime.Now;
                    ToursComputing.LinKernighan(nodes, JourneyController.Project.LKNearestNeighbors, JourneyController.Project.LKGap);
                    afterTime = DateTime.Now;
                    Logs.Notify((afterTime - preTime).ToString(@"hh\:mm\:ss\:fff"), 0);
                    JourneyController.TSP.Nodes = nodes;
                    DrawGraph();
                    LogGraphInfo();
                    LogOptimumDeviation();
                }
                catch (Exception exception)
                {
                    throw new LinKernighanErrorException("Ошибка при вычислении тура алгоритмом Лина-Кернигана:\r\n" + exception.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                ErrorHandle.DoHandle(exception);
            }

        }

        //========

        private void mmiShowBestDistance_Click(object sender, EventArgs e)
        {
            MessageBox.Show(JourneyController.TSP.Nodes.BestCost.ToString());
        }

        private void mmiDrawCurrentTour_Click(object sender, EventArgs e)
        {
            DrawGraph();
            LogGraphInfo();
        }

        //========

        private void mmiTesting_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    DoTesting();
                }
                catch (Exception exception)
                {
                    throw new NearestNeighborErrorException("Произошла ошибка при тестировании:\r\n" + exception.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                ErrorHandle.DoHandle(exception);
            }
        }

        //========

        private void mmiAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        //==============================================================================

        /* Действия при создании формы. */
        public MainForm()
        {
            InitializeComponent();

            // Устанавливаем TextBox как обработчик логов.
            Logs.Output = tbLogs;
            Logs.LogLevel = 3;
            UpdateCaption();
            UpdateState(false);
            tbLogs.Clear();

            // В целях быстрой отладки сразу загружаем проект. 
            //OpenProject("..\\..\\tests\\test.jproj");
        }

        //==============================================================================
        // Drag'n'Drop

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {

            string[] s = (string[])e.Data.GetData("FileDrop", false);
            if (s.Count() != 0)
            {
                CloseProject(false);
                OpenProject(s[0]);
            }
        }
    }
}