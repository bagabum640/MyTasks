﻿using System;
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
<<<<<<< HEAD

=======
    
>>>>>>> b0c8e9a7c23313aa24dab1d2e826a81a84c75801
    class Player
    {
        public int Health;
        public int Armor;
        public int Damage;

        public Player()
        {
            Health = 100;
            Armor = 20;
            Damage = 35;
        }

        public Player(int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Здоровье: {Health}\nБроня: {Armor}\nУрон: {Damage}");
        }
    }
}