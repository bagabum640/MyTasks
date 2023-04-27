using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchCriminal
{
    class Program
    {
        static void Main(string[] args)
        {
            CriminalDatabase criminalDatabase = new CriminalDatabase();

            criminalDatabase.Work();                        
        }
    }

    class CriminalDatabase
    {
        private readonly List<Criminal> _criminals = new List<Criminal>();
        private readonly List<Criminal> _filteredCriminals = new List<Criminal>();
        private readonly Dictionary<string, string> _filters = new Dictionary<string, string>();
        private int _cursorPositionX;
        private int _cursorPositionY;

        public CriminalDatabase()
        {
            _criminals.Add(new Criminal("Джони Две Куртки", false, 170, 90, "Француз"));
            _criminals.Add(new Criminal("Майк Кислые Яйца", true, 192, 81, "Англичанин"));
            _criminals.Add(new Criminal("Джошуа Липкий Язык", false, 167, 50, "Француз"));
            _criminals.Add(new Criminal("Кейси Дрявый Кисет", true, 176, 72, "Француз"));
            _criminals.Add(new Criminal("Василий Комок Блох", false, 173, 70, "Русский"));
            _criminals.Add(new Criminal("Артем Крестный Отец", false, 187, 90, "Русский"));
            _criminals.Add(new Criminal("Радик Закатанный Таз", true, 162, 53, "Русский"));
            _criminals.Add(new Criminal("Сэмюэль Желтый Снег", false, 191, 87, "Англичанин"));
            _filteredCriminals.AddRange(_criminals);
        }

        public void Work()
        {            
            const string CommandShowCriminals = "Показать/скрыть преступников.";
            const string CommandFilter = "Фильтровать.";
            const string CommandResetFilters = "Удалить фильтры.";            
            const string CommandExit = "Выход.";

            bool isShowCriminals = false;
            bool isWork = true;                      
            string[] commands = { CommandShowCriminals, CommandFilter, CommandResetFilters, CommandExit };

            while (isWork)
            {
                ShowFilters();

                if (isShowCriminals)
                    ShowCriminals();

                ReadCursorPosition();

                switch (ChooseCommand(commands))
                {
                    case CommandShowCriminals:
                        isShowCriminals ^= true;                        
                        break;

                    case CommandFilter:
                        Filter();
                        break;

                    case CommandResetFilters:                        
                        RemoveFilters();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }
            }
        }

        private void Filter()
        {
            const string CommandHidePrisoners = "Скрыть пойманных преступников.";
            const string CommandHeightFilter = "Фильтровать по росту.";
            const string CommandWeightFilter = "Фильтровать по весу.";
                        
            string[] commands = { CommandHidePrisoners , CommandHeightFilter , CommandWeightFilter };

            ReadCursorPosition();

            switch (ChooseCommand(commands))
            {
                case CommandHidePrisoners:
                    HidePrisoners();
                    break;

                case CommandHeightFilter:
                    FilterHeight();
                    break;

                case CommandWeightFilter:
                    FilterWeight();
                    break;
            }
        }

        private void FilterHeight()
        {
            const string CommandTakeMoreThen = "Показать преступников, чей рост больше заданного.";
            const string CommandTakeLessThen = "Показать преступников, чей рост меньше заданного.";

            string command;
            string filterName = "unknown";
            char sign = ' ';
            string[] commands = { CommandTakeMoreThen, CommandTakeLessThen };
            List<Criminal> criminals = new List<Criminal>();

            //Console.Clear();
            ReadCursorPosition();
            command = ChooseCommand(commands);
            Console.Write("Введите рост: ");

            if (int.TryParse((Console.ReadLine()), out int parameter))
            {
                Console.Clear();

                switch (command)
                {
                    case CommandTakeMoreThen:
                        var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Height > parameter);
                        sign = '>';
                        filterName = "heightFilterMore";
                        criminals.AddRange(filteredCriminals);
                        break;

                    case CommandTakeLessThen:
                        filteredCriminals = _filteredCriminals.Where(criminal => criminal.Height < parameter);
                        sign = '<';
                        filterName = "heightFilterLess";
                        criminals.AddRange(filteredCriminals);
                        break;
                }

                AddFilter(filterName, $"Фильтр по росту. Показаны преступники чей рост {sign} {parameter} см.", criminals);
            }
            else
            {
                ShowValueError();
            }
        }

        private void FilterWeight()        
        {
            const string CommandTakeMoreThen = "Показать преступников, чей вес больше заданного.";
            const string CommandTakeLessThen = "Показать преступников, чей вес меньше заданного.";

            string command;
            string filterName = "unknown";
            char sign = ' ';
            string[] commands = { CommandTakeMoreThen, CommandTakeLessThen };
            List<Criminal> criminals = new List<Criminal>();

            Console.Clear();
            ReadCursorPosition();
            command = ChooseCommand(commands);
            Console.Write("Введите вес: ");           
            
            if (int.TryParse((Console.ReadLine()), out int parameter))
            {
                Console.Clear();                

                switch (command)
                {
                    case CommandTakeMoreThen:                        
                        var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Weight > parameter);
                        sign = '>';
                        filterName = "weightFilterMore";                        
                        criminals.AddRange(filteredCriminals);
                        break;

                    case CommandTakeLessThen:                        
                        filteredCriminals = _filteredCriminals.Where(criminal => criminal.Weight < parameter);
                        sign = '<';
                        filterName = "weightFilterLess";
                        criminals.AddRange(filteredCriminals);
                        break;
                }
                
                AddFilter(filterName, $"Фильтр по весу. Показаны преступники чей вес {sign} {parameter} кг.", criminals);                          
            }
            else
            {
                ShowValueError();
            }    
        }               

        private void HidePrisoners()
        {
            string filterName = "hidePrisoners";
            string message = "Пойманные преступники скрыты.";
            List<Criminal> criminals = new List<Criminal>();

            var filteredCriminals = _filteredCriminals.Where(criminal => criminal.IsPrisoner == false);         
            criminals.AddRange(filteredCriminals);
            AddFilter(filterName, message, criminals);                      
        }
                
        private void RemoveFilters()
        {
            _filteredCriminals.Clear();
            _filteredCriminals.AddRange(_criminals);
            _filters.Clear();
        }

        private void ShowFilters()
        {
            foreach (var filter in _filters)
            {
                Console.WriteLine(filter.Value);
            }

            if (_filters.Count > 0)
            {
                Console.WriteLine();
            }            
        }

        private void AddFilter(string filterName, string description, List<Criminal> filteredCriminals)
        {
            if (_filters.ContainsKey(filterName))
            {
                _filters[filterName] = description;                
            }
            else
            {
                _filters.Add(filterName, description);
            }

            _filteredCriminals.Clear();
            _filteredCriminals.AddRange(filteredCriminals);
        }

        private void ShowCriminals()
        {
            Console.WriteLine("Преступники на свободе:");

            foreach (var criminal in _filteredCriminals)
            {
                criminal.ShowInfo();
            }

            Console.WriteLine();
        }

        private string ChooseCommand(string[] commands)
        {
            const ConsoleKey PreviousString = ConsoleKey.UpArrow;
            const ConsoleKey NextString = ConsoleKey.DownArrow;
            const ConsoleKey SelectString = ConsoleKey.Enter;

            bool isWork = true;
            int numberString = 0;
            ConsoleKeyInfo key;

            while (isWork)
            {
                Console.SetCursorPosition(_cursorPositionX, _cursorPositionY);
                WriteStrings(0, numberString, commands);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(commands[numberString]);
                Console.ResetColor();
                WriteStrings(numberString + 1, commands.Length, commands);
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case SelectString:
                        isWork = false;
                        break;

                    case NextString:
                        numberString = (numberString + 1 < commands.Length) ? numberString + 1 : 0;
                        break;

                    case PreviousString:
                        numberString = (numberString - 1 >= 0) ? numberString - 1 : commands.Length - 1;
                        break;
                }
            }

            ClearPartOfConsole(_cursorPositionX, _cursorPositionY, commands);
            return commands[numberString];
        }

        private void WriteStrings(int firstString, int lastString, string[] strings)
        {
            for (int i = firstString; i < lastString; i++)
            {
                Console.WriteLine(strings[i]);
            }
        }

        private void ClearPartOfConsole(int сursorPositionX, int сursorPositionY, string[] commands)
        {
            Console.SetCursorPosition(сursorPositionX, сursorPositionY);

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < commands[i].Length; j++)
                {
                    Console.Write(' ');
                }

                Console.WriteLine("");
            }

            Console.Clear();
        }

        private void ReadCursorPosition()
        {
            _cursorPositionX = Console.CursorLeft;
            _cursorPositionY = Console.CursorTop;
        }

        private void ShowValueError()
        {
            Console.Clear();
            Console.WriteLine("Не удается определить значение!");
            Console.ReadKey(true);
            Console.Clear();
        }
    }

    class Criminal
    {
        public Criminal(string name, bool isPrisoner, int height, int weight, string nationality)
        {
            FullName = name;
            IsPrisoner = isPrisoner;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string FullName { get; private set; }
        public bool IsPrisoner { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"Имя:{FullName}. Рост:{Height} см. Вес:{Weight} кг. Национальность:{Nationality}. ");

            if (IsPrisoner)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("В заключении.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("На свободе.\n");
                Console.ResetColor();
            }
        }
    }
}
