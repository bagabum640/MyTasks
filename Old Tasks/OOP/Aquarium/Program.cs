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
            
            aquarium.Work();
        }
    }

    class Aquarium
    {
        private const string AddFishCommand = "Добавить рыбку";
        private const string RemoveFishCommand = "Убрать рыбку";
        private const string ExitCommand = "Отойти от аквариума";

        private string[] _commands = { AddFishCommand, RemoveFishCommand, ExitCommand };
        private string _command;
        private List<Fish> _fishes = new List<Fish>();
        private uint _aquariumCapacity = 10;
        private Menu _menu;
        private bool _isWatch = true;
        private FishBuilder _fishBuilder;

        public Aquarium()
        {
            int initialQuntityOfFish = 5;
            _menu = new Menu();
            _fishBuilder = new FishBuilder();

            for (int i = 0; i < initialQuntityOfFish; i++)
            {
                _fishes.Add(_fishBuilder.BuildFish());
            }
        }

        public void Work()
        {
            while (_isWatch)
            {
                ShowFishes();
                SkipYear();
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

            if (int.TryParse(Console.ReadLine(), out int fishNumber) && fishNumber-1 < _fishes.Count && fishNumber > 0)
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
                
        private void SkipYear()
        {
            foreach (var fish in _fishes)
            {
                fish.Grow();
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
                _fishes.Add(_fishBuilder.BuildFish());
            else
                Console.WriteLine("Аквариум полон!");
        }
    }

    class FishBuilder
    {
        private int _minLifeExpectancy = 10;
        private int _maxLifeExpectancy = 15;
        private string[] _possibleNames = { "Джимми", "Робби", "Немо", "Цуи", "Фанни", "Пиппи", "Ломс" };
        private static Random _random = new Random();

        public Fish BuildFish()
        {
            Fish fish = new Fish(_possibleNames[_random.Next(_possibleNames.Length)], (uint)_random.Next(_minLifeExpectancy, _maxLifeExpectancy));

            return fish;
        }
    }

    class Fish
    {              
        private uint _currentAge = 0;
        private uint _maxAge;
        private string _name;

        public Fish(string name, uint maxAge)
        {
            _name = name;
            _maxAge = maxAge;
            IsAlive = true;
        }

        public bool IsAlive { get; private set; }

        public void ShowInfo()
        {
            if (IsAlive)            
                Console.WriteLine($"Рыбка {_name}: текущий возраст {_currentAge} лет.");
            else
                Console.WriteLine($"Рыбка {_name}. Умерла в возрасте {_maxAge} лет.");            
        }

        public void Grow()
        {
            _currentAge++;
            KillFish();
        }                

        private void KillFish()
        {
            IsAlive = _currentAge < _maxAge; 
        }
    }

    class Menu
    {
        public string ChooseCommand(string[] commands)
        {
            bool isWork = true;
            int numberString = 0;
            int CursorPositionX = 0;
            int CursorPositionY = Console.CursorTop + 2;

            while (isWork)
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
                        isWork = false;
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
