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

            battleGround.CalculateBattle();
        }
    }

    abstract class Fighter
    {
        protected int _chancePool = 100;
        protected Random _random = new Random();

        public Fighter(string name, int health, int damage, int armor, bool melee)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = MaxHealth;
            Damage = damage;
            Armor = armor;
            Melee = melee;
        }

        public string Name { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int Damage { get; private set; }
        public int Armor { get; private set; }
        public bool Melee { get; private set; }

        public virtual void Attack(Fighter target)
        {
            Console.WriteLine($"{Name} машет мечом и наносит {Damage} единиц урона.");
            target.TakeDamage(Damage);
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
            if (CurrentHealth > 0)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write($"{Name} {CurrentHealth}/{MaxHealth}    ");
            Console.ResetColor();            
        }
    }

    class Barbarian : Fighter
    {
        private double _criticalDamageMultiplier = 2.5;
        private int _criticalStrikeChance = 50;        

        public Barbarian(string name) : base(name, 400, 60, 15, true) { }

        public override void Attack(Fighter target)
        {            
            int criticalDamage = (int)(Damage * _criticalDamageMultiplier);

            if (_random.Next(_chancePool) >= _chancePool - _criticalStrikeChance)
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

        public Knight(string name) : base(name, 550, 45, 30, true) { }

        public override void TakeDamage(int damage)
        {
            if (_random.Next(_chancePool) >= _chancePool - _blockChance && damage > 0)
            {
                Console.WriteLine($"{Name} укрывается за щитом, уменьшая повреждения на половину!");
                damage = (int)(_blockDamageMultiplier * damage);
            }

            base.TakeDamage(damage);
        }
    }

    class Mag : Fighter
    {
        private int _maxMana = 60;
        private int _currentMana;
        private int _regenerationMana = 15;
        private int _magicDamage = 180;
        private int _fireBallCost = 25;

        public Mag(string name) : base(name, 300, 35, 10, false) 
        { 
            _currentMana = _maxMana; 
        }

        public override void Attack(Fighter target)
        {
            if (TryCastFireBall(target))
            {                
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

        private bool TryCastFireBall(Fighter target)
        {
            if (_currentMana >= _fireBallCost)
            {
                Console.WriteLine($"{Name} бросает огненый шар, нанося {_magicDamage} единиц урона.");
                _currentMana -= _fireBallCost;
                target.TakeDamage(_magicDamage);
                return true;
            }

            return false;
        }
    }

    class CrossbowMan : Fighter
    {
        private bool _isCrossbowLoaded = true;

        public CrossbowMan(string name) : base(name, 380, 120, 20, false) { }

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
        private int _healingPower = 80;
        private int _chanceToCastPray = 50;

        public Priest(string name) : base(name, 320, 50, 15, false) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Pray();
        }

        private void Pray()
        {
            if (_random.Next(_chancePool) >= _chancePool - _chanceToCastPray)
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

    class Squad
    {
        private int _fighterIndex = 0;
        private Random _random = new Random();
        private List<Fighter> _fighters = new List<Fighter>();
        private List<Fighter> _fightersAtGunpoint = new List<Fighter>();

        public Squad(string squadname)
        {
            _fighters.Add(new Barbarian($"{squadname} варвар"));
            _fighters.Add(new Knight($"{squadname} рыцарь"));
            _fighters.Add(new Mag($"{squadname} маг"));
            _fighters.Add(new CrossbowMan($"{squadname} арбалетчик"));
            _fighters.Add(new Priest($"{squadname} жрец"));
        }

        public bool IsAlive()
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

        public void ShowFighters(int cursorStringNumber, int cursorOffset)
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.SetCursorPosition(cursorOffset, cursorStringNumber);
                _fighters[i].ShowInfo();                
                cursorStringNumber++;
            }
        }

        public void Attack (Squad targets)
        {
            if (_fighters[_fighterIndex].CurrentHealth > 0)
            {
                Fighter target = targets.ProvideTarget(_fighters[_fighterIndex].Melee);

                if (target != null)
                    _fighters[_fighterIndex].Attack(target);
            }

            CountFighterIndex();
        }              

        public Fighter ProvideTarget(bool isMeleeAttack)
        {
            Fighter target;

            if (isMeleeAttack)
            {
                if (TryFindMeleeTargets() != true)
                    FindTarget();
            }
            else
            {
                FindTarget();
            }
                        
            target = _fightersAtGunpoint[_random.Next(_fightersAtGunpoint.Count)];

            return target;
        }

        private bool TryFindMeleeTargets()
        {
            _fightersAtGunpoint.Clear();

            foreach (var fighter in _fighters)
            {
                if (fighter.Melee && fighter.CurrentHealth > 0)
                {
                    _fightersAtGunpoint.Add(fighter);
                }
            }

            if (_fightersAtGunpoint.Count > 0)
                return true;
            else
                return false;
        }

        private void FindTarget()
        {
            _fightersAtGunpoint.Clear();

            foreach (var fighter in _fighters)
            {
                if (fighter.CurrentHealth > 0)
                {
                    _fightersAtGunpoint.Add(fighter);
                }
            }
        }

        private void CountFighterIndex()
        {
            _fighterIndex++;

            if (_fighterIndex >= _fighters.Count())
            {
                _fighterIndex = 0;
            }            
        }
    }

    class BattleGround
    {
        private Squad _whiteFighters;
        private Squad _blackFighters;
        private int _battlelogStringNumber = 10;

        public BattleGround()
        {
            _whiteFighters = new Squad("Белый");
            _blackFighters = new Squad("Черный");            
        }

        public void CalculateBattle()
        {
            ShowFighters();

            while (_whiteFighters.IsAlive() && _blackFighters.IsAlive())
            {
                Console.Clear();
                Console.SetCursorPosition(0, _battlelogStringNumber);
                _whiteFighters.Attack(_blackFighters);
                Console.WriteLine();
                _blackFighters.Attack(_whiteFighters);
                ShowFighters();
                Console.ReadKey();                
            }

            CongratulateWinner();
        }

        private void CongratulateWinner()
        {
            Console.Clear();

            if (_whiteFighters.IsAlive() == true && _blackFighters.IsAlive() == false)
            {
                Console.WriteLine("Победа за белыми! Они сокрушили врага!");
            }
            else if (_whiteFighters.IsAlive() == false && _blackFighters.IsAlive() == true)
            {
                Console.WriteLine("Победа за черными! Они сокрушили врага!");
            }
            else if (_whiteFighters.IsAlive() == false && _blackFighters.IsAlive() == false)
            {
                Console.WriteLine("Какой ужас, все мертвы!");
            }
        }
                        
        private void ShowFighters()
        {
            int cursorStringNumber = 0;
            int cursorOffset = 50;

            _whiteFighters.ShowFighters(cursorStringNumber, 0);
            _blackFighters.ShowFighters(cursorStringNumber, cursorOffset);

            Console.WriteLine("\n");
            Console.ResetColor();
        }
    }
}
