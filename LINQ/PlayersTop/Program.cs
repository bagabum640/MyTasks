using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayersTop
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerDatabase serverDatabase = new ServerDatabase(); 
        }
    }

    class ServerDatabase
    {
        private List<Player> _players = new List<Player>();

        public ServerDatabase()
        {
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("НубСайбот", 13, 76));
            _players.Add(new Player("Анка Пулеметчица", 63, 126));
            _players.Add(new Player("Чахлик", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
            _players.Add(new Player("Васька Читер", 80, 666));
        }
    }

    class Player
    {
        public Player(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {Name}. Уровень: {Level}. Уровень силы: {Power}.");
        }
    }
}
