using System;
using System.Collections.Generic;

namespace GladiatorFight
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ShowFightersCommand = "показать бойцов";
            const string ChooseFighterCommand = "выбрать бойца";
            const string ResetCommand = "сбросить выбор";
            const string StartCombatCommand = "начать бой";
            const string ExitCommand = "выйти";

            bool isPlay = true;
            string command;
            string[] commands = { ShowFightersCommand, ChooseFighterCommand, ResetCommand, StartCombatCommand, ExitCommand };

            Arena arena = new Arena();

            arena.ShowGreeting();

            while (isPlay)
            {
                arena.ShowMainBar();
                command = arena.ChooseCommand(commands);

                switch (command)
                {
                    case ShowFightersCommand:
                        arena.ShowFighters();
                        break;

                    case ChooseFighterCommand:
                        arena.ChooseFighters();
                        break;

                    case ResetCommand:
                        arena.ResetFightersChoice();
                        break;

                    case StartCombatCommand:
                        arena.StartCombat();
                        break;

                    case ExitCommand:
                        isPlay = false;
                        break;
                }

                Console.Clear();
            }
        }
    }

    abstract class Fighter
    {
        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = MaxHealth;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public virtual void Attack(Fighter target)
        {
            Console.WriteLine($"{Name} машет мечом и наносит {Damage} единиц урона.");
            target.TakeDamage(Damage);
        }

        public virtual void TakeDamage(int damage)
        {
            int resultingDamage = (damage - Armor <= 0) ? 0 : damage - Armor;
            CurrentHealth -= resultingDamage;

            if (damage > 0)
            {
                Console.WriteLine($"Броня смягчает урон, {Name} получает {resultingDamage} единиц урона.");
            }
        }

        public virtual void RestoreCharacteristics(int quantityHealth)
        {
            if (MaxHealth - CurrentHealth < quantityHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += quantityHealth;
            }
        }

        public void ShowInfo()
        {
            Console.Write($"{Name} {CurrentHealth}/{MaxHealth}    ");
        }
    }

    class Barbarian : Fighter
    {
        private double _criticalDamageMultiplier = 2.5;
        private int _criticalStrikeChance = 40;
        private int _strikeChance = 100;

        public Barbarian() : base("Варвар", 400, 60, 15) { }

        public override void Attack(Fighter target)
        {
            Random random = new Random();
            int criticalDamage = (int)(Damage * _criticalDamageMultiplier);

            if (random.Next(_strikeChance) < _criticalStrikeChance)
            {
                Console.WriteLine($"{Name} кружится в смертельном танце, рассекая врага и нанося {criticalDamage} единиц урона!");
                target.TakeDamage(criticalDamage);
                return;
            }

            Console.WriteLine($"{Name} рубит с плеча нанося {Damage} единиц урона.");
            target.TakeDamage(Damage);
        }
    }

    class Knight : Fighter
    {
        private int _blockChance = 50;
        private double _blockDamageMultiplier = 0.5;

        public Knight() : base("Рыцарь", 550, 45, 30) { }

        public override void TakeDamage(int damage)
        {
            Random random = new Random();

            if (random.Next(100) > 100-_blockChance && damage > 0)
            {
                Console.WriteLine($"{Name} укрывается за щитом, уменьшая повреждения на половину!");
                damage = (int)(_blockDamageMultiplier*damage);
            }

            base.TakeDamage(damage);
        }
    }

    class Mag : Fighter
    {
        private static int _maxMana = 60;
        private int _currentMana = _maxMana;
        private int _regenerationMana = 15;
        private int _magicDamage = 180;
        private int _fireBallCost = 25;

        public Mag() : base("Маг", 300, 35, 10) { }

        public override void Attack(Fighter target)
        {
            if (TryCastFireBall())
            {
                target.TakeDamage(_magicDamage);
                return;
            }

            Console.WriteLine($"{Name} размахивает своим посохом, нанося {Damage} единиц урона и восстанавливая {_regenerationMana} маны.");
            _currentMana += _regenerationMana;
            target.TakeDamage(Damage);
        }

        public override void RestoreCharacteristics(int quantityHealth)
        {
            _currentMana = _maxMana;
            base.RestoreCharacteristics(quantityHealth);
        }

        private bool TryCastFireBall()
        {
            if (_currentMana >= _fireBallCost)
            {
                Console.WriteLine($"{Name} бросает огненый шар, нанося {_magicDamage} единиц урона.");
                _currentMana -= _fireBallCost;
                return true;
            }

            return false;
        }
    }

    class CrossbowMan : Fighter
    {
        private bool _isCrossbowLoaded = true;

        public CrossbowMan() : base("Арбалетчик", 380, 120, 20) { }

        public override void Attack(Fighter target)
        {
            if (_isCrossbowLoaded)
            {
                _isCrossbowLoaded = false;
                Console.WriteLine($"{Name} стреляет в противника и наносит {Damage} единиц урона.");
                target.TakeDamage(Damage);
                return;
            }

            Console.WriteLine($"{Name} перезаряжает арбалет.");
            _isCrossbowLoaded = true;
            target.TakeDamage(0);
        }
    }

    class Priest : Fighter
    {
        private int _healingPower = 90;
        private int _chanceToCastPray = 60;
        private int _generalChanceOfPull = 100;

        public Priest() : base("Жрец", 320, 50, 15) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Pray();
        }

        private void Pray()
        {
            Random random = new Random();

            if (random.Next(_generalChanceOfPull) < _chanceToCastPray)
            {
                Console.WriteLine($"{Name} вздымает руки к небу и Боги внемлят его молитвам восстанавливая здоровье.");
                RestoreCharacteristics(_healingPower);
            }
            else
            {
                Console.WriteLine($"{Name} вздымает руки к небу, но Боги его не слышат.");
            }
        }
    }

    class Arena
    {
        private int _cursorPositionX;
        private int _cursorPositionY;
        private Fighter _firstFighter;
        private Fighter _secondFighter;
        private List<Fighter> _fighters = new List<Fighter>();

        public Arena()
        {
            _fighters.Add(new Barbarian());
            _fighters.Add(new Knight());
            _fighters.Add(new Mag());
            _fighters.Add(new CrossbowMan());
            _fighters.Add(new Priest());
        }

        public void ShowFighters()
        {
            int cursorPositionX = 0;
            int cursorPositionY = 5;

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);

            foreach (var fighter in _fighters)
            {
                if (fighter.CurrentHealth > 0)
                {
                    Console.WriteLine($"{fighter.Name}:\nHP: {fighter.MaxHealth}\nУрон: {fighter.Damage}\nБроня: {fighter.Armor}\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{fighter.Name} - МЕРТВ\n");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nНажмите любую клавишу чтобы продолжить.");
            Console.ReadKey(true);
            Console.Clear();
        }

        public Fighter FindFighter(string name)
        {
            foreach (var fighter in _fighters)
            {
                if (name == fighter.Name)
                {
                    if (IsAlive(fighter))
                    {
                        return fighter;
                    }
                }
            }

            return null;
        }

        public string[] ShowFightersNames()
        {
            string[] names = new string[_fighters.Count];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = _fighters[i].Name;
            }

            return names;
        }

        public void ChooseFighters()
        {
            const string FirstFighterCommand = "первый боец";
            const string SecondFighterCommand = "второй боец";

            string[] fighters = { FirstFighterCommand, SecondFighterCommand };
            string fighterNumber;

            fighterNumber = ChooseCommand(fighters);

            if (fighterNumber == FirstFighterCommand)
            {
                TryChooseFighter(_firstFighter, _secondFighter);
            }
            else if (fighterNumber == SecondFighterCommand)
            {
                TryChooseFighter(_secondFighter, _firstFighter);
            }
        }

        public void ResetFightersChoice()
        {
            _firstFighter = null;
            _secondFighter = null;
        }

        public void StartCombat()
        {
            if (CheckFighters())
            {
                int roundNumber = 1;
                _cursorPositionX = 0;
                _cursorPositionY = 4;

                Console.Clear();
                ShowMainBar();

                while (_firstFighter.CurrentHealth > 0 && _secondFighter.CurrentHealth > 0)
                {
                    Console.SetCursorPosition(_cursorPositionX, _cursorPositionY);
                    RunRound(roundNumber);
                    ShowMainBar();
                    roundNumber++;
                    Console.ReadKey();
                }

                CongratulateTheWinner();
                Console.ReadKey();
                ResetFightersChoice();
            }
        }

        public void ShowMainBar()
        {
            int firstFighterPositionX = 0;
            int firstFighterPositionY = 0;
            int secondFighterPositionX = 40;
            int secondFighterPositionY = 0;
            char borderSymbol = '_';
            uint borderLength = 55;
            int borderStringNumber = 2;
            int nextTextStringNumber = borderStringNumber + 2;

            Console.SetCursorPosition(firstFighterPositionX, firstFighterPositionY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            CheckNull(_firstFighter);
            Console.SetCursorPosition(secondFighterPositionX, secondFighterPositionY);
            Console.ForegroundColor = ConsoleColor.Blue;
            CheckNull(_secondFighter);
            Console.ResetColor();
            Console.SetCursorPosition(0, borderStringNumber);

            for (int i = 0; i < borderLength; i++)
            {
                Console.Write(borderSymbol);
            }

            Console.SetCursorPosition(0, nextTextStringNumber);
        }

        public void ShowGreeting()
        {
            Console.WriteLine("Добро пожаловать на нашу арену! \n\nСегодня здесь состоятся бои наших претендентов за звание короля арены!\nНажмите любую клавишу чтобы продолжить.");
            Console.ReadKey(true);
            Console.Clear();
        }

        public string ChooseCommand(string[] commands)
        {
            bool isLeaveMenu = false;
            int numberString = 0;
            int CursorPositionX = 0;
            int CursorPositionY = 5;

            while (!isLeaveMenu)
            {
                Console.SetCursorPosition(CursorPositionX, CursorPositionY);

                for (int i = 0; i < numberString; i++)
                {
                    Console.WriteLine(commands[i]);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
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

        private void CheckNull(Fighter fighter)
        {
            if (fighter == null)
            {
                Console.Write("Пусто");
            }
            else
            {
                fighter.ShowInfo();
            }
        }

        private bool IsAlive(Fighter fighter)
        {
            if (fighter.CurrentHealth > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Боец мертв!");
                Console.ReadKey();
                return false;
            }
        }

        private void RunRound(int roundNumber)
        {
            Console.WriteLine($"---------------Раунд {roundNumber}---------------\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            _firstFighter.Attack(_secondFighter);
            Console.ForegroundColor = ConsoleColor.Blue;
            _secondFighter.Attack(_firstFighter);
            Console.ResetColor();
            _cursorPositionX = Console.CursorLeft;
            _cursorPositionY = Console.CursorTop + 1;
        }

        private void CongratulateTheWinner()
        {
            Console.Clear();

            if (_firstFighter.CurrentHealth > 0 && _secondFighter.CurrentHealth <= 0)
            {
                Console.WriteLine($"И у нас победитель!!! Это {_firstFighter.Name}!!!");
                _firstFighter.RestoreCharacteristics(_firstFighter.MaxHealth);
            }
            else if (_secondFighter.CurrentHealth > 0 && _firstFighter.CurrentHealth <= 0)
            {
                Console.WriteLine($"И у нас победитель!!! Это {_secondFighter.Name}!!!");
                _secondFighter.RestoreCharacteristics(_secondFighter.MaxHealth);
            }
            else if (_firstFighter.CurrentHealth <= 0 && _secondFighter.CurrentHealth <= 0)
            {
                Console.WriteLine("Ох, какая жалость! Они убили друг друга!");
            }
        }

        private bool CheckFighters()
        {
            if (_firstFighter != null && _secondFighter != null)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Выберите бойцов для боя!");
                Console.ReadKey();

                return false;
            }
        }

        private void TryChooseFighter(Fighter trueFighter, Fighter wrongFighter)
        {
            Fighter fighter = FindFighter(ChooseCommand(ShowFightersNames()));

            if (fighter == wrongFighter && wrongFighter != null)
            {
                Console.WriteLine("Боец уже выбран!");
                Console.ReadKey();
            }
            else
            {
                if (trueFighter == _firstFighter)
                {
                    _firstFighter = fighter;
                }
                else if (trueFighter == _secondFighter)
                {
                    _secondFighter = fighter;
                }
            }
        }
    }
}