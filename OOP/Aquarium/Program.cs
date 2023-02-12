using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Lifing();
        }
    }

    class Aquarium
    {
        const string AddFishCommand = "Добавить рыбку";
        const string RemoveFishCommand = "Убрать рыбку";
        const string ExitCommand = "Отойти от аквариума";

        private string[] _commands = { AddFishCommand, RemoveFishCommand, ExitCommand };
        private string _command;
        private List<Fish> _fishes = new List<Fish>();
        private uint _aquariumCapacity = 10;
        private Menu _menu;
        private bool _isWatch = true;

        public Aquarium()
        {
            _menu = new Menu();

            for (int i = 0; i < 5; i++)
            {
                _fishes.Add(new Fish());
            }
        }

        public void Lifing()
        {
            while (_isWatch)
            {
                ShowFishes();
                ShipYear();
                _command = _menu.ChooseCommand(_commands);

                switch (_command)
                {
                    case AddFishCommand:
                        AddFish();
                        break;

                    case RemoveFishCommand:
                        RemoveFish();
                        break;

                    case ExitCommand:
                        _isWatch = false;
                        break;

                    default:
                        break;
                }
                                
                Console.Clear();
            }
        }

        private void RemoveFish()
        {            
            Console.Write("Введите номер рыбки, от которой хотите избавиться: ");

            if (int.TryParse(Console.ReadLine(), out int fishNumber) && fishNumber-1 < _fishes.Count)
            {
                _fishes.RemoveAt(fishNumber - 1);
            }                
            else
            {
                Console.Clear();
                Console.WriteLine("Неверное значение!");
                Console.ReadLine();
            }
        }

        
        private void ShipYear()
        {
            foreach (var fish in _fishes)
            {
                fish.ToGrow();
            }
        }

        private void ShowFishes()
        {
            int fishNumber = 1;

            foreach (var fish in _fishes)
            {
                if (fish.IsAlive == false)                
                    Console.ForegroundColor = ConsoleColor.Red;                
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.Write($"{fishNumber} ");
                fish.ShowInfo();
                fishNumber++;
            }

            Console.ResetColor();
        }

        private void AddFish()
        {
            if(_fishes.Count < _aquariumCapacity)
                _fishes.Add(new Fish());
            else
                Console.WriteLine("Аквариум полон!");
        }
    }

    class Fish
    {
        private static Random random = new Random();
        private uint _maxAge;
        private uint _currentAge;
        private string _name;
        private string [] _possibleNames = {"Джимми", "Робби","Немо", "Цуи","Фанни","Пиппи","Ломс"};        
        private int _minLifeExpectancy = 10;
        private int _maxLifeExpectancy = 15;

        public bool IsAlive { get; private set; }

        public Fish()
        {
            ToBorn();
        }

        public void ShowInfo()
        {
            if (IsAlive)            
                Console.WriteLine($"Рыбка {_name}: текущий возраст {_currentAge} лет.");
            else
                Console.WriteLine($"Рыбка {_name}. Умерла в возрасте {_maxAge} лет.");            
        }

        public void ToGrow()
        {
            _currentAge++;
            CheckToAlive();
        }

        private void ToBorn()
        {
            _name = _possibleNames[random.Next(_possibleNames.Length)];
            _currentAge = 0;
            IsAlive = true;
            _maxAge = (uint)random.Next(_minLifeExpectancy, _maxLifeExpectancy);
        }

        private void CheckToAlive()
        {
            IsAlive = (_currentAge >= _maxAge) ? false : true; 
        }
    }

    class Menu
    {
        public string ChooseCommand(string[] commands)
        {
            bool isLeaveMenu = false;
            int numberString = 0;
            int CursorPositionX = 0;
            int CursorPositionY = Console.CursorTop + 2;

            while (!isLeaveMenu)
            {
                Console.SetCursorPosition(CursorPositionX, CursorPositionY);

                for (int i = 0; i < numberString; i++)
                {
                    Console.WriteLine(commands[i]);
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(commands[numberString]);
                Console.ResetColor();

                for (int i = numberString + 1; i < commands.Length; i++)
                {
                    Console.WriteLine(commands[i]);
                }

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        isLeaveMenu = true;
                        break;

                    case ConsoleKey.DownArrow:
                        numberString = (numberString + 1 < commands.Length) ? numberString + 1 : 0;
                        break;

                    case ConsoleKey.UpArrow:
                        numberString = (numberString - 1 >= 0) ? numberString - 1 : commands.Length - 1;
                        break;

                    default:
                        break;
                }
            }

            ClearPartOfConsole(CursorPositionX, CursorPositionY, commands);
            return commands[numberString];
        }

        private void ClearPartOfConsole(int CursorPositionX, int CursorPositionY, string[] commands)
        {
            Console.SetCursorPosition(CursorPositionX, CursorPositionY);

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < commands[i].Length; j++)
                {
                    Console.Write(' ');
                }

                Console.WriteLine();
            }
        }
    }
}
