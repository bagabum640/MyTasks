using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.Work();
        }
    }

    class Zoo
    {
        private const string CommandExit = "Выход";

        private readonly List<Aviary> _aviaries = new List<Aviary>();
        private readonly List<string> _commands = new List<string>();

        public Zoo()
        {
            _aviaries.Add(new Aviary("Клетка с обезьянами", "Обезьяна", 3, 2, "У-у"));
            _aviaries.Add(new Aviary("Клетка с бегемотами", "Бегемот", 1, 1, "Мэ-мэ"));
            _aviaries.Add(new Aviary("Клетка с петухами", "Петух", 3, 2, "Ку-ка-ре-ку"));
            _aviaries.Add(new Aviary("Клетка с морскими котиками", "Морской котик", 3, 2, "Мяу (по-морскому)"));
            _aviaries.Add(new Aviary("Клетка с попугаями", "Попугай", 7, 8, "Чик-чирик"));            
            FillCommandList();
        }

        public void Work()
        {
            string command;
            bool isWork = true;
            Aviary aviary;

            while (isWork)
            {
                command = ChooseCommand(_commands.ToArray());

                if (command == CommandExit)
                {
                    isWork = false;
                    continue;
                }

                aviary = FindAviary(command);

                if (aviary != null)
                    aviary.ShowDescription();

                Console.Clear();
            }
        }

        private Aviary FindAviary(string aviaryName)
        {        
            foreach (Aviary aviary in _aviaries)
            {
                if (aviary.Name == aviaryName)
                {                    
                    return aviary;
                }
            }

            return null;
        }

        private void FillCommandList()
        {
            foreach (var aviary in _aviaries)
            {
                _commands.Add(aviary.Name);
            }

            _commands.Add(CommandExit);
        }

        private string ChooseCommand(string[] commands)
        {
            const ConsoleKey PreviousString = ConsoleKey.UpArrow;
            const ConsoleKey NextString = ConsoleKey.DownArrow;
            const ConsoleKey SelectString = ConsoleKey.Enter;

            bool isWork = true;
            int numberString = 0;
            int CursorPositionX = 0;
            int CursorPositionY = Console.CursorTop;
            ConsoleKeyInfo key;

            while (isWork)
            {
                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                WriteStrings(0, numberString, commands);
                Console.ForegroundColor = ConsoleColor.Yellow;
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

            ClearPartOfConsole(CursorPositionX, CursorPositionY, commands);
            Console.SetCursorPosition(0, 0);
            return commands[numberString];
        }

        private void WriteStrings(int firstString, int lastString, string[] strings)
        {
            for (int i = firstString; i < lastString; i++)
            {
                Console.WriteLine(strings[i]);
            }
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

    class Aviary
    {
        private string _animalName;
        private uint _maleQuantity;
        private uint _femaleQuantity;
        private string _emittedSound;

        public Aviary(string cageName, string animalName, uint maleQuantity, uint femaleQuantity, string emittedSound)
        {
            Name = cageName;
            _animalName = animalName;
            _maleQuantity = maleQuantity;
            _femaleQuantity = femaleQuantity;
            _emittedSound = emittedSound;
        }

        public string Name { get; private set; }

        public void ShowDescription()
        {
            string description = $"{Name}\nВ клетке содержится животное: {_animalName}\n" +
                $"Количество самцов в клетке: {_maleQuantity}\nКоличество самок в клетке: {_femaleQuantity}\n" +
                $"Животное издает звук: {_emittedSound}\n\n";

            Console.WriteLine(description);
            Console.ReadKey();
        }
    }
}
