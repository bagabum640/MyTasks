using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    class Program
    {
        static void Main(string[] args)
        {
            Prison prison = new Prison();

            prison.Work();
        }
    }

    class Prison
    {        
        private readonly List<Prisoner> _prisoners = new List<Prisoner>();

        public Prison()
        {
            _prisoners.Add(new Prisoner("Джони Две Куртки", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Майк Кислые Яйца", "Сел на пенек"));
            _prisoners.Add(new Prisoner("Джошуа Липкий Язык",  "Кинул пацана"));
            _prisoners.Add(new Prisoner("Кейси Дрявый Кисет",  "Не толерантен"));
            _prisoners.Add(new Prisoner("Василий Комок Блох", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Артем Крестный Отец", "Антиправительственное"));
            _prisoners.Add(new Prisoner("Радик Закатанный Таз", "Шумел после десяти"));
            _prisoners.Add(new Prisoner("Сэмюэль Желтый Снег", "Не ел суп"));
        }            

        public void Work()
        {
            const string CommandFreePrisoner = "Выдать амнистию";
            const string CommandExit = "Покинуть тюрьму";

            string[] commands = { CommandFreePrisoner, CommandExit };
            bool isWork = true;
                       
            ShowPrisoners(_prisoners);
            StopShowing();

            while (isWork)
            {
                switch (ChooseCommand(commands))
                {
                    case CommandFreePrisoner:
                        FreePrisoners();
                        break;
                    case CommandExit:
                        isWork = false;
                        break;
                }                
            }
        }

        private void FreePrisoners()
        {
            List<Prisoner> freePeople = new List<Prisoner>();
            string offence = "Антиправительственное";

            freePeople.AddRange(_prisoners.Where(prisoner => prisoner.Offense == offence).ToList());
            Console.WriteLine("Выпущены по амнистии:");
            ShowPrisoners(freePeople);
            Console.WriteLine("\nОставшиеся в заключении:");
            ShowPrisoners(_prisoners.Except(freePeople).ToList());
            StopShowing();
        }

        private void ShowPrisoners(List<Prisoner> prisoners)
        {
            foreach (var prisoner in prisoners)
            {
                prisoner.Show();
            } 
        }

        private void StopShowing()
        {
            Console.ReadKey();
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

    class Prisoner
    {
        public Prisoner(string name, string offense)
        {
            Name = name;
            Offense = offense;
        }

        public string Name { get; private set; }
        public string Offense { get; private set; }

        public void Show()
        {
            Console.WriteLine($"Имя: {Name}. Преступление: {Offense}.");
        }
    }
}

