using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Player newPlayer = new Player();
            newPlayer.ShowStats();
        }
    }

    class Player
    {
        private int _health;
        private int _armor;
        private int _damage;

        public Player()
        {
            _health = 100;
            _armor = 20;
            _damage = 35;
        }

        public Player(int health, int armor, int damage)
        {
            _health = health;
            _armor = armor;
            _damage = damage;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Здоровье: {_health}\nБроня: {_armor}\nУрон: {_damage}");
        }
    }
}
