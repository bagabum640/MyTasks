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
        }

        public void Work()
        {
            const string CommandFilter = "Фильтровать";
            const string CommandExit = "Выйти";

            bool isWork = true;
            string[] commands = { CommandFilter, CommandExit };

            while (isWork)
            {
                ShowCriminals(_criminals.ToArray());

                switch (ChooseCommand(commands))
                {
                    case CommandFilter:
                        Filter();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }
            }
        }

        private void Filter()
        {                  
            RequestParameter("Введите минимальный рост: ", out int minHeight);           
            RequestParameter("Введите максимальный рост: ", out int maxHeight);  
            RequestParameter("Введите минимальный вес: ", out int minWeight);
            RequestParameter("Введите максимальный вес: ", out int maxWeight);            
            Console.WriteLine("Выберите национальность: ");
            string nationality = DetermineNationality();
            
            var filteredCriminals = from Criminal criminal in _criminals
                                    where criminal.IsPrisoner == false
                                    where criminal.Height > minHeight
                                    where criminal.Height < maxHeight
                                    where criminal.Weight > minWeight
                                    where criminal.Weight < maxWeight
                                    where criminal.Nationality == nationality
                                    select criminal;

            Console.WriteLine();
            ShowCriminals(filteredCriminals.ToArray());            
        }

        private bool RequestParameter(string message, out int volue)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out volue))
            {
                return true;
            }

            Console.Clear();
            Console.WriteLine("Не удается определить значение!");
            Console.ReadKey(true);
            Console.Clear();
            return false;
        }

        private string DetermineNationality()
        {
            List<string> nationalities = new List<string>();

            foreach (var criminal in _criminals)
            {
                nationalities.Add(criminal.Nationality);
            }

            var differentNationalities = nationalities.Distinct();

            return ChooseCommand(differentNationalities.ToArray());
        }

        private void ShowCriminals(Criminal [] criminals)
        {
            if (criminals.Length <= 0)
                Console.WriteLine("Преступников с заданными параметрами не найдено.");
            else
                Console.WriteLine("Список преступников:");

            foreach (var criminal in criminals)
            {
                criminal.ShowInfo();
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить.");
            Console.ReadKey(true);
            Console.Clear();            
        }

        private string ChooseCommand(string[] commands)
        {
            const ConsoleKey PreviousString = ConsoleKey.UpArrow;
            const ConsoleKey NextString = ConsoleKey.DownArrow;
            const ConsoleKey SelectString = ConsoleKey.Enter;

            bool isWork = true;
            int numberString = 0;
            int cursorPositionX = Console.CursorLeft;
            int cursorPositionY = Console.CursorTop;
            ConsoleKeyInfo key;

            if (IsWrongArray(commands))
                return null;

            while (isWork)
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
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

            ClearPartOfConsole(cursorPositionX, cursorPositionY, commands);
            return commands[numberString];
        }

        private bool IsWrongArray(string[] array)
        {
            if (array == null || array.Length == 0)
                return true;

            foreach (var command in array)
            {
                if (command == null)
                    return true;
            }

            return false;
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

            Console.SetCursorPosition(сursorPositionX, сursorPositionY);            
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
