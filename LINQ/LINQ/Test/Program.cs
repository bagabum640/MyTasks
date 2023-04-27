using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
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
        private const string FilterHidePrisoner = "hidePrisoner";
        private const string FilterWeightMore = "weightMore";
        private const string FilterWeightLess = "weightLess";
        private const string FilterHeightMore = "heightMore";
        private const string FilterHeightLess = "heightLess";
        private const string NationalityFilter = "nationality";

        private readonly List<Criminal> _criminals = new List<Criminal>();
        private readonly List<Criminal> _filteredCriminals = new List<Criminal>();
        private readonly List<Filter> _filters = new List<Filter>();
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
            const string CommandCreateFilter = "Фильтровать.";
            const string CommandResetFilters = "Удалить фильтры.";
            const string CommandExit = "Выход.";

            bool isShowCriminals = false;
            bool isWork = true;
            string[] commands = { CommandShowCriminals, CommandCreateFilter, CommandResetFilters, CommandExit };

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

                    case CommandCreateFilter:
                        ChooseFilters();
                        break;

                    case CommandResetFilters:
                        RemoveFilters();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                ApplyFilters();
            }
        }

        private void ChooseFilters()
        {
            const string CommandSwitchHidePrisoners = "Показать/скрыть пойманных преступников.";
            const string CommandHeightFilter = "Фильтровать по росту.";
            const string CommandWeightFilter = "Фильтровать по весу.";
            const string CommandNationalityFilter = "Фильтровать по национальности.";

            string[] commands = { CommandSwitchHidePrisoners, CommandHeightFilter, CommandWeightFilter, CommandNationalityFilter };
            string heightParameter = "height";
            string weightParameter = "weight";

            ReadCursorPosition();

            switch (ChooseCommand(commands))
            {
                case CommandSwitchHidePrisoners:
                    SwitchHidePrisonersFilter();
                    break;

                case CommandHeightFilter:
                    DesignateBiometrics(heightParameter);
                    break;

                case CommandWeightFilter:
                    DesignateBiometrics(weightParameter);
                    break;

                case CommandNationalityFilter:
                    CreateNationalityFilter();
                    break;
            }
        }

        private void ApplyFilters()
        {
            WriteFilteredCriminals(_criminals);

            if (TryFindFilter(FilterHidePrisoner, out Filter filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.IsPrisoner == false);
                WriteFilteredCriminals(filteredCriminals.ToList());
            }

            if (TryFindFilter(FilterHeightMore, out filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Height > Convert.ToInt32(filter.Value));
                WriteFilteredCriminals(filteredCriminals.ToList());
            }

            if (TryFindFilter(FilterHeightLess, out filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Height < Convert.ToInt32(filter.Value));
                WriteFilteredCriminals(filteredCriminals.ToList());
            }

            if (TryFindFilter(FilterWeightMore, out filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Weight > Convert.ToInt32(filter.Value));
                WriteFilteredCriminals(filteredCriminals.ToList());
            }

            if (TryFindFilter(FilterWeightLess, out filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Weight < Convert.ToInt32(filter.Value));
                WriteFilteredCriminals(filteredCriminals.ToList());
            }

            if (TryFindFilter(NationalityFilter, out filter))
            {
                var filteredCriminals = _filteredCriminals.Where(criminal => criminal.Nationality == filter.Value);
                WriteFilteredCriminals(filteredCriminals.ToList());
            }
        }

        private bool TryFindFilter(string filterName, out Filter findedFilter)
        {
            foreach (var filter in _filters)
            {
                if (filter.Name == filterName)
                {
                    findedFilter = filter;
                    return true;
                }
            }

            findedFilter = null;
            return false;
        }

        private void DesignateFilterValue(string filterName, int parameter)
        {
            if (TryFindFilter(filterName, out Filter filter))
            {
                filter.Value = parameter.ToString();
                SetMessage(filter);
            }
            else
            {
                filter = new Filter(filterName) { Value = parameter.ToString() };
                SetMessage(filter);
                _filters.Add(filter);
            }
        }

        private void DesignateBiometrics(string biometrics)
        {
            const string CommandHeightMoreThen = "Показать преступников, чей рост больше заданного.";
            const string CommandHeightLessThen = "Показать преступников, чей рост меньше заданного.";
            const string CommandWeightMoreThen = "Показать преступников, чей вес больше заданного.";
            const string CommandWeightLessThen = "Показать преступников, чей вес меньше заданного.";
            const string ParameterHeight = "height";
            const string ParameterWeight = "weight";

            string command;
            int quntityCommand = 2;
            string[] commands = new string[quntityCommand];

            switch (biometrics)
            {
                case ParameterHeight:
                    commands = new string[] { CommandHeightMoreThen, CommandHeightLessThen };
                    break;

                case ParameterWeight:
                    commands = new string[] { CommandWeightMoreThen, CommandWeightLessThen };
                    break;
            }

            Console.Clear();
            ReadCursorPosition();
            command = ChooseCommand(commands);
            Console.Write("Введите параметр: ");

            if (int.TryParse((Console.ReadLine()), out int parameter))
            {
                Console.Clear();

                switch (command)
                {
                    case CommandHeightMoreThen:
                        DesignateFilterValue(FilterHeightMore, parameter);
                        break;

                    case CommandHeightLessThen:
                        DesignateFilterValue(FilterHeightLess, parameter);
                        break;

                    case CommandWeightMoreThen:
                        DesignateFilterValue(FilterWeightMore, parameter);
                        break;

                    case CommandWeightLessThen:
                        DesignateFilterValue(FilterWeightLess, parameter);
                        break;
                }
            }
            else
            {
                ShowValueError();
            }
        }

        private void SwitchHidePrisonersFilter()
        {
            if (TryFindFilter(FilterHidePrisoner, out Filter filter))
            {
                _filters.Remove(filter);
            }
            else
            {
                filter = new Filter(FilterHidePrisoner);
                SetMessage(filter);
                _filters.Add(filter);
            }
        }

        private void CreateNationalityFilter()
        {
            if (TryFindFilter(NationalityFilter, out Filter filter))
            {
                filter.Value = DetermineNationality();
                SetMessage(filter);
            }
            else
            {
                filter = new Filter(NationalityFilter) { Value = DetermineNationality() };
                SetMessage(filter);
                _filters.Add(filter);
            }
        }

        private string DetermineNationality()
        {
            List<string> nationalities = new List<string>();

            foreach (var criminal in _criminals)
            {
                nationalities.Add(criminal.Nationality);
            }

            var differentNationalities = nationalities.Distinct();

            if (differentNationalities.Count() > 0)
                return ChooseCommand(differentNationalities.ToArray());

            return null;
        }

        private void SetMessage(Filter filter)
        {
            switch (filter.Name)
            {
                case FilterHidePrisoner:                    
                    filter.Message = "Пойманные преступники скрыты.";
                    break;

                case FilterWeightMore:
                    filter.Message = $"Включен фильтр по весу. Показаны преступники чей вес > {filter.Value} кг.";
                    break;

                case FilterWeightLess:
                    filter.Message = $"Включен фильтр по весу. Показаны преступники чей вес < {filter.Value} кг.";
                    break;

                case FilterHeightMore:
                    filter.Message = $"Включен фильтр по росту. Показаны преступники чей вес > {filter.Value} кг.";
                    break;

                case FilterHeightLess:
                    filter.Message = $"Включен фильтр по росту. Показаны преступники чей вес < {filter.Value} кг.";
                    break;

                case NationalityFilter:
                    filter.Message = $"Включен фильтр по национальности. Показаны преступники с нации {filter.Value}.";
                    break;
            }
        }

        private void RemoveFilters()
        {
            _filters.Clear();
        }

        private void ShowFilters()
        {
            foreach (var filter in _filters)
            {
                Console.WriteLine(filter.Message);
            }

            if (_filters.Count > 0)
            {
                Console.WriteLine();
            }
        }

        private void WriteFilteredCriminals(List<Criminal> filteredCriminals)
        {
            _filteredCriminals.Clear();
            _filteredCriminals.AddRange(filteredCriminals);
        }

        private void ShowCriminals()
        {
            Console.WriteLine("Список преступников:");

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

    class Filter
    {
        public Filter(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public string Message { get; set; }        
        public string Value { get; set; }
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
            Console.Write($"Имя: {FullName}. Рост: {Height} см. Вес: {Weight} кг. Национальность: {Nationality}. ");

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

