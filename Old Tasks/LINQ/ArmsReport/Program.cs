using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmsReport
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
        private readonly List<Soldier> _soldiers = new List<Soldier>();

        public Army()
        {
            _soldiers.Add(new Soldier("Борис", "Винтовка", "Сержант", 18));
            _soldiers.Add(new Soldier("Иван", "Автомат", "Рядовой", 9));
            _soldiers.Add(new Soldier("Николай", "Винтовка", "Сержант", 15));
            _soldiers.Add(new Soldier("Тимофей", "Гранатамет", "Рядовой", 8));
            _soldiers.Add(new Soldier("Артем", "Автомат", "Лейтенант", 23));
            _soldiers.Add(new Soldier("Василий", "Огнемет", "Рядовой", 12));
        }

        public void Work()
        {
            var soldierData = from Soldier soldier in _soldiers
                              select new
                              {
                                  soldier.Name,
                                  soldier.Rank,
                              };

            foreach (var data in soldierData)
            {
                Console.WriteLine($"Имя: {data.Name}. \tЗвание: {data.Rank}.");
            }
        }
    }

    class Soldier
    {
        public Soldier(string name, string armament, string rank , int lifeTime)
        {
            Name = name;
            Armament = armament;
            Rank = rank;
            LifeTime = lifeTime;
        }

        public string Name { get; private set; }
        public string Armament { get; private set; }
        public string Rank { get; private set; }
        public int LifeTime { get; private set; }        
    }
}
