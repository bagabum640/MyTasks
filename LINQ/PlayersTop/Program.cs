using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayersTop
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerDatabase serverDatabase = new ServerDatabase();

            serverDatabase.Work();
        }
    }

    class ServerDatabase
    {
        private readonly List<Player> _players = new List<Player>();

        public ServerDatabase()
        {
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("НубСайбот", 13, 26));
            _players.Add(new Player("Анка Пулеметчица", 63, 126));
            _players.Add(new Player("Чахлик", 70, 140));
            _players.Add(new Player("Питер Смоук", 26, 55));
            _players.Add(new Player("Винсент Поп", 50, 103));
            _players.Add(new Player("Чикивара", 37, 74));
            _players.Add(new Player("Моник Слоник", 28, 57));
            _players.Add(new Player("Мистер Кротер", 32, 65));
            _players.Add(new Player("Кракен", 44, 88));
        }

        public void Work()
        {
            const string CommandShowTopLvl = "Показать топ 3 игроков по уровню";
            const string CommandShowTopPower = "Показать топ 3 игроков по силе";
            const string CommandExit = "Выход";

            string[] commands = { CommandShowTopLvl , CommandShowTopPower , CommandExit };
            bool isWork = true;

            while (isWork)
            {                
                ShowPlayers(_players);
                Console.WriteLine();

                switch (ChooseCommand(commands))
                {
                    case CommandShowTopLvl:
                        ShowTopLvl();
                        break;

                    case CommandShowTopPower:
                        ShowTopPower();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }
            }            
        }

        private void ShowTopLvl()
        {
            Console.Clear();
            ShowPlayers(_players.OrderByDescending(player => player.Level).Take(3).ToList());
            StopShowing();
        }

        private void ShowTopPower()
        {
            Console.Clear();
            ShowPlayers(_players.OrderByDescending(player => player.Power).Take(3).ToList());
            StopShowing();
        }

        private void StopShowing()
        {
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                player.ShowInfo();
            }
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

                Console.WriteLine();
            }

            Console.SetCursorPosition(сursorPositionX, сursorPositionY);
        }
    }

    class Player
    {
        public Player(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {Name}. Уровень: {Level}. Уровень силы: {Power}.");
        }
    }
}
