using System;
using System.Collections.Generic;
using System.Linq;

namespace UnificationTroops
{
    class Program
    {
        static void Main(string[] args)
        {
            Army army = new Army();

            army.Work();
        }
    }

    class Army
    {
        private readonly List<Soldier> _firstSquad = new List<Soldier>();
        private readonly List<Soldier> _secondSquad = new List<Soldier>();

        public Army()
        {
            _firstSquad.Add(new Soldier("Иванов Борис"));
            _firstSquad.Add(new Soldier("Серов Иван"));
            _firstSquad.Add(new Soldier("Петров Николай"));
            _firstSquad.Add(new Soldier("Большаков Тимофей"));
            _firstSquad.Add(new Soldier("Безруков Артем"));
            _firstSquad.Add(new Soldier("Колбасин Василий"));
            _secondSquad.Add(new Soldier("Фликов Семен"));
            _secondSquad.Add(new Soldier("Горов Виталий"));
            _secondSquad.Add(new Soldier("Шиков Алексей"));
            _secondSquad.Add(new Soldier("Иванов Константин"));
        }

        public void Work()
        {
            ShowSoldiers(_firstSquad, _secondSquad);

            Console.WriteLine("\nБойцы из первого отряда переведены во второй.\n");

            ShowSoldiers(_firstSquad.Except(_firstSquad.Where(soldier => soldier.Name.StartsWith("Б"))).ToList(), 
                         _secondSquad.Union(_firstSquad.Where(soldier => soldier.Name.StartsWith("Б"))).ToList());
        }

        private void ShowSoldiers(List<Soldier> firstSquad, List<Soldier> secondSquad)
        {
            Console.WriteLine("Бойцы первого отряда:\n");

            foreach (var soldier in firstSquad)
            {
                Console.WriteLine(soldier.Name);
            }

            Console.WriteLine("\nБойцы второго отряда:\n");

            foreach (var soldier in secondSquad)
            {
                Console.WriteLine(soldier.Name);
            }
        }
    }

    class Soldier
    {
        public Soldier(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
