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
            ArenaController arenaController = new ArenaController();
            Menu menu = new Menu();            

            menu.ShowGreeting();
            
            while (isPlay)
            {
                menu.ShowMainBar(arenaController);
                command = menu.ChooseCommand(commands);

                switch (command)
                {
                    case ShowFightersCommand:
                        arena.ShowFighters();
                        break;

                    case ChooseFighterCommand:
                        arenaController.ChooseFighters(arena, menu);
                        break;

                    case ResetCommand:
                        arenaController.ResetFightersChoice();
                        break;

                    case StartCombatCommand:
                        arenaController.StartCombat(menu, arenaController);
                        break;

                    case ExitCommand:
                        isPlay = false;
                        break;

                    default:
                        break;
                }

                Console.Clear();
            }
        }
    }

    class Fighter
    {        
        public string Name { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }

        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = MaxHealth;
            Damage = damage;
            Armor = armor;
        }

        public virtual int Attack()
        {
            Console.WriteLine($"{Name} машет мечом и наносит {Damage} единиц урона.");
            return Damage;
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

        public virtual void RestoreHealh(int quantityHealth)
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
        public Barbarian() : base ("Варвар", 400, 60, 15) { }

        public override int Attack()
        {
            Random random = new Random();
            int criticalDamage = (int)(Damage * 2.5);
            
            if (random.Next(10) > 6)
            {
                Console.WriteLine($"{Name} кружится в смертельном танце, рассекая врага и нанося {criticalDamage} единиц урона!");
                return criticalDamage;
            }

            Console.WriteLine($"{Name} рубит с плеча нанося {Damage} единиц урона.");
            return Damage;
        }
    }

    class Knight : Fighter
    {
        public Knight() : base ("Рыцарь", 550, 45, 30) { }

        public override void TakeDamage(int damage)
        {
            Random random = new Random();

            if (random.Next(10) > 5 && damage >0)
            {
                Console.WriteLine($"{Name} укрывается за щитом, уменьшая повреждения на половину!");
                damage /= 2;
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

        public override int Attack()
        {       
            if (TryCastFireBall())
            {
                return _magicDamage;
            }

            Console.WriteLine($"{Name} размахивает своим посохом, нанося {Damage} единиц урона и восстанавливая {_regenerationMana} маны.");
            _currentMana += _regenerationMana;
            return Damage;
        }

        public override void RestoreHealh(int quantityHealth)
        {            
            _currentMana = _maxMana;
            base.RestoreHealh(quantityHealth);
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

        public CrossbowMan() : base ("Арбалетчик", 380, 120, 20) { }

        public override int Attack()
        {
            if (_isCrossbowLoaded)
            {
                _isCrossbowLoaded = false;
                Console.WriteLine($"{Name} стреляет в противника и наносит {Damage} единиц урона.");
                return Damage;
            }

            Console.WriteLine($"{Name} перезаряжает арбалет.");
            _isCrossbowLoaded = true;
            return 0;            
        }
    }

    class Priest : Fighter
    {
        int _healingPower = 90;

        public Priest() : base("Жрец", 320, 50, 15) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Pray();  
        }

        private void Pray()
        {
            Random random = new Random();

            if (random.Next(9) > 4)
            {
                Console.WriteLine($"{Name} вздымает руки к небу и Боги внемлят его молитвам восстанавливая здоровье.");
                RestoreHealh(_healingPower);
            }
            else
            {
                Console.WriteLine($"{Name} вздымает руки к небу, но Боги его не слышат.");
            }
        }
    }

    class Arena
    {
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
            Console.SetCursorPosition(0, 5);
                        
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
                    if (CheckAlive(fighter))
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

        private bool CheckAlive (Fighter fighter)
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
    }

    class ArenaController
    {
        public Fighter FirstFighter { get ;  private set; }
        public Fighter SecondFighter { get; private set; }

        public void ChooseFighters(Arena arena, Menu menu)
        {
            const string FirstFighterCommand = "первый боец";
            const string SecondFighterCommand = "второй боец";

            string[] fighters = { FirstFighterCommand, SecondFighterCommand };
            string fighterNumber;            

            fighterNumber = menu.ChooseCommand(fighters);

            if (fighterNumber == FirstFighterCommand)
            {
                TryChooseFighter(arena, menu, FirstFighter, SecondFighter);                
            }
            else if (fighterNumber == SecondFighterCommand)
            {
                TryChooseFighter(arena, menu, SecondFighter, FirstFighter);                
            }
        }

        public void ResetFightersChoice()
        {
            FirstFighter = null;
            SecondFighter = null;
        }

        public void StartCombat(Menu menu, ArenaController arenaController)
        {
            if (CheckFighters())
            {
                int roundNumber = 1;
                int cursorPositionX = 0;
                int cursorPositionY = 4;

                Console.Clear();
                menu.ShowMainBar(arenaController);

                while (FirstFighter.CurrentHealth > 0 && SecondFighter.CurrentHealth > 0)
                {                    
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    RunRound(roundNumber, ref cursorPositionX, ref cursorPositionY);
                    menu.ShowMainBar(arenaController);                    
                    roundNumber++;
                    Console.ReadKey();
                }

                CongraduationToWinner();
                Console.ReadKey();
                ResetFightersChoice();
            }            
        }

        private void RunRound (int roundNumber, ref int cursorPositionX, ref int cursorPositionY)
        {
            Console.WriteLine($"---------------Раунд {roundNumber}---------------\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            SecondFighter.TakeDamage(FirstFighter.Attack());
            Console.ForegroundColor = ConsoleColor.Blue;
            FirstFighter.TakeDamage(SecondFighter.Attack());
            Console.ResetColor();
            cursorPositionX = Console.CursorLeft;
            cursorPositionY = Console.CursorTop + 1;
        }

        private void CongraduationToWinner()
        {
            Console.Clear();

            if (FirstFighter.CurrentHealth > 0 && SecondFighter.CurrentHealth <= 0)
            {
                Console.WriteLine($"И у нас победитель!!! Это {FirstFighter.Name}!!!");
                FirstFighter.RestoreHealh(FirstFighter.MaxHealth);
            }
            else if (SecondFighter.CurrentHealth > 0 && FirstFighter.CurrentHealth <= 0)
            {
                Console.WriteLine($"И у нас победитель!!! Это {SecondFighter.Name}!!!");
                SecondFighter.RestoreHealh(SecondFighter.MaxHealth);
            }
            else if (FirstFighter.CurrentHealth <= 0 && SecondFighter.CurrentHealth <= 0)
            {
                Console.WriteLine("Ох, какая жалость! Они убили друг друга!");
            }
        }

        private bool CheckFighters()
        {
            if (FirstFighter != null && SecondFighter != null)
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

        private void TryChooseFighter(Arena arena, Menu menu, Fighter trueFighter, Fighter wrongFighter)
        {
            Fighter fighter = arena.FindFighter(menu.ChooseCommand(arena.ShowFightersNames()));
            
            if (fighter == wrongFighter && wrongFighter!= null)
            {
                Console.WriteLine("Боец уже выбран!");
                Console.ReadKey();
            }
            else
            {
                if (trueFighter == FirstFighter)
                {
                    FirstFighter = fighter;
                }
                else if (trueFighter == SecondFighter)
                {
                    SecondFighter = fighter;
                }                                
            }
        }
    }

    class Menu
    {      
        public void ShowMainBar(ArenaController arenaController)
        {
            char borderSymbol = '_';
            uint borderLength = 55;
            int borderStringNumber = 2;
            int nextTextStringNumber = borderStringNumber + 2;

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            CheckNull(arenaController.FirstFighter);
            Console.SetCursorPosition(40, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            CheckNull(arenaController.SecondFighter);
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
    }        
}