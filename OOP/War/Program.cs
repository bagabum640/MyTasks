using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleGround battleGround = new BattleGround();

            battleGround.StartBattle();
        }
    }

    abstract class Fighter
    {
        public string Name { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }
        public bool Melee { get; private set; }

        public Fighter(string name, int health, int damage, int armor, bool melee)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = MaxHealth;
            Damage = damage;
            Armor = armor;
            Melee = melee;
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

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

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
        public Barbarian(string name) : base(name, 400, 60, 15, true) { }

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
        public Knight(string name) : base(name, 550, 45, 30, true) { }

        public override void TakeDamage(int damage)
        {
            Random random = new Random();

            if (random.Next(10) > 5 && damage > 0)
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

        public Mag(string name) : base(name, 300, 35, 10, false) { }

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

        public CrossbowMan(string name) : base(name, 380, 120, 20, false) { }

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

        public Priest(string name) : base(name, 320, 50, 15, false) { }

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

    class BattleGround
    {
        private List<Fighter> _whiteFighters = new List<Fighter>();
        private List<Fighter> _blackFighters = new List<Fighter>();
        private List<Fighter> _targets = new List<Fighter>();
        private Random random = new Random();            

        public BattleGround()
        {
            _whiteFighters.Add(new Barbarian("Белый варвар"));
            _whiteFighters.Add(new Knight("Белый рыцарь"));
            _whiteFighters.Add(new Mag("Белый маг"));
            _whiteFighters.Add(new CrossbowMan("Белый арбалетчик"));
            _whiteFighters.Add(new Priest("Белый жрец"));
            _blackFighters.Add(new Barbarian("Черный варвар"));
            _blackFighters.Add(new Knight("Черный рыцарь"));
            _blackFighters.Add(new Mag("Черный маг"));
            _blackFighters.Add(new CrossbowMan("Черный арбалетчик"));
            _blackFighters.Add(new Priest("Черный жрец"));
        }

        public void StartBattle()
        {
            ShowFighters();

            while (CheckToAlive(_whiteFighters) && CheckToAlive(_blackFighters))
            {
                RunRound();
            }

            CongraduationToWinner();
        }

        private void CongraduationToWinner()
        {
            Console.Clear();

            if (CheckToAlive(_whiteFighters) == true && CheckToAlive(_blackFighters) == false)
            {
                Console.WriteLine("Победа за белыми! Они сокрушили врага!");
            }
            else if (CheckToAlive(_whiteFighters) == false && CheckToAlive(_blackFighters) == true)
            {
                Console.WriteLine("Победа за черными! Они сокрушили врага!");
            }
            else if (CheckToAlive(_whiteFighters) == false && CheckToAlive(_blackFighters) == false)
            {
                Console.WriteLine("Какой ужас, все мертвы!");
            }
        }

        private bool CheckToAlive(List<Fighter> _fighters)
        {
            foreach (var fighter in _fighters)
            {
                if (fighter.CurrentHealth > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void RunRound()
        {
            Fighter fighter;

            for (int i = 0; i < _whiteFighters.Count; i++)
            {
                if (_whiteFighters[i].CurrentHealth > 0)
                {
                    fighter = ChooseTarget(_blackFighters, _whiteFighters[i]);

                    if (fighter != null)
                        fighter.TakeDamage(_whiteFighters[i].Attack());
                }

                Console.WriteLine();

                if (_blackFighters[i].CurrentHealth > 0)
                {
                    fighter = ChooseTarget(_whiteFighters, _blackFighters[i]);

                    if (fighter != null)                    
                        fighter.TakeDamage(_blackFighters[i].Attack());   
                }

                Console.ReadKey();
                Console.Clear();
                ShowFighters();
            }
        }

        private Fighter ChooseTarget(List<Fighter> targets, Fighter attackingFighter)
        {
            Fighter fighter;

            if (attackingFighter.Melee)
            {
                if (TryFindMeleeTargets(targets) != true)
                    FindTarget(targets);
            }
            else
            {
                FindTarget(targets);
            }

            if (_targets.Count > 0)
            {
                fighter = _targets[random.Next(_targets.Count)];
            }
            else
            {
                fighter = null;
            }
            
            return fighter;
        }

        private bool TryFindMeleeTargets(List<Fighter> targets)
        {
            _targets.Clear();

            foreach (var fighter in targets)
            {                
                if (fighter.Melee && fighter.CurrentHealth > 0)
                {
                    _targets.Add(fighter);                    
                }
            }

            if (_targets.Count > 0)            
                return true;                            
            else            
                return false;                            
        }

        private void FindTarget(List<Fighter> targets)
        {
            _targets.Clear();

            foreach (var fighter in targets)
            {
                if (fighter.CurrentHealth > 0)
                {
                    _targets.Add(fighter);
                }
            }
        }

        private void ShowFighters()
        {
            int cursorStringNumber = 0;
            int cursorOffset = 50;

            for (int i = 0; i < _whiteFighters.Count; i++)
            {
                Console.SetCursorPosition(0, cursorStringNumber);

                if (_whiteFighters[i].CurrentHealth > 0)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                _whiteFighters[i].ShowInfo();
                Console.SetCursorPosition(cursorOffset, cursorStringNumber);

                if (_blackFighters[i].CurrentHealth > 0)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.Red;

                _blackFighters[i].ShowInfo();
                cursorStringNumber++;
            }

            Console.WriteLine("\n");
            Console.ResetColor();
        }
    }
}
