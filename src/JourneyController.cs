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
using System.IO;

using JourneyIO;
using JourneyExceptions;

namespace JourneyGUI
{
    public class JourneyController
    {
        // Предотвращаем создание экземпляра класса путём создания защищённого конструктора. 
        protected JourneyController() { }
        
        private sealed class JourneyControllerCreator
        {
            private static readonly JourneyController instance = new JourneyController();

            public static JourneyController Instance
            {
                get { return instance; }
            }
        }

        public static JourneyController Instance
        {
            get { return JourneyControllerCreator.Instance; }
        }

        private JourneyProject project = null;
        private TSPLib _TSP = null;

        public static JourneyProject Project
        {
            get
            {
                if (Instance.project == null)
                {
                    throw new ObjectIsNotInitializedException("Project не был инициализирован!");
                }
                else
                {
                    return Instance.project;
                }
            }
        }
        public static TSPLib TSP
        {
            get
            {
                if (Instance._TSP == null)
                {
                    throw new ObjectIsNotInitializedException("TSP не был инициализирован!");
                }
                else
                {
                    return Instance._TSP;
                }
            }
        }
        public bool IsInitialized
        {
            get
            {
                if ((Instance.project != null) && (Instance._TSP != null))
                {
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string ProjectName
        {
            get
            {
                if (IsInitialized)
                {
                    return Instance.project.ProjectFileName;
                }
                else
                {
                    throw new ObjectIsNotInitializedException("Journey Controller не был инициализирован!");
                }
            }
        }


        /* Создание объектов проекта и задачи и их чтение. */
        public static void Initialize(string projectFileName)
        {
            Instance.project = new JourneyProject(projectFileName);
            // Если указан относительный путь задачи, то загружаем её относительно файла проекта.
            if (!Path.IsPathRooted(Instance.project.ProblemFileName))
                Instance.project.ProblemFileName = Path.GetDirectoryName(projectFileName) + "/" + Instance.project.ProblemFileName;
            Instance._TSP = new TSPLib(Instance.project.ProblemFileName);
        }

        public void Close()
        {
            if (Instance._TSP != null)
            {
                Instance._TSP.Nodes.ClearNodes();
                Instance._TSP.Nodes = null;
            }
            Instance.project = null;
            Instance._TSP = null;
        }
    }
}
