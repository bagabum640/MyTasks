using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            const string AddPlayerCommand = "add";
            const string ShowPlayersCommand = "show";
            const string ChangeActivePlayerCommand = "ca";            
            const string DeletePlayerCommand = "delete";
            const string ExitCommand = "exit";

            string command;
            bool isExit = false;
            DataBase dataBase = new DataBase();

            Console.WriteLine($"Enter:\n{AddPlayerCommand} - to add new player\n{ShowPlayersCommand} - to show all players\n{ChangeActivePlayerCommand} - to ban player, used him id\n" +
                $"{DeletePlayerCommand} - to delete player, used him id\n{ExitCommand} - to exit");
            
            while (isExit == false)
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case AddPlayerCommand:
                        dataBase.AddPlayer();
                        break;

                    case ShowPlayersCommand:
                        dataBase.ShowPlayers();
                        break;

                    case ChangeActivePlayerCommand:
                        dataBase.ChangeActivePlayer();
                        break;

                    case DeletePlayerCommand:
                        dataBase.DeletePlayer();
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Wrong command!");
                        break;
                }
            }            
        }
    }

    class DataBase
    {
        private List <Player> _players = new List<Player>();
        private int _countAccounts;

        public void AddPlayer ()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            _countAccounts++;
            Player player = new Player (_countAccounts, name);
            _players.Add(player);            
        }

        public void ShowPlayers()
        {
            foreach (var player in _players)
            {
                player.ShowStats();
            }
        }

        public void ChangeActivePlayer()
        {
            Console.Write("Enter id: ");

            if (ReadNumber(out int id))
            {
                foreach (var player in _players)
                {
                    if (player.ID == id)
                    {
                        player.ChangeActive();
                    }                    
                }
            }
        }

        public void DeletePlayer()
        {
            Console.Write("Enter id: ");

            if (ReadNumber(out int id))
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].ID == id)
                    {
                        _players.RemoveAt(i);
                    }                    
                }
            }            
        }

        private bool ReadNumber (out int id)
        {
            if (int.TryParse(Console.ReadLine(), out id))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong id!");
                return false;
            }
        }
    }

    class Player
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public bool isActive { get; private set; }

        public Player (int id, string name)
        {
            ID = id;
            Name = name;
            Level = 1;
            isActive = true;
        }

        public void ShowStats()
        {
            Console.WriteLine($"ID: {ID}\nName: {Name}\nLevel:{Level}\nActive: {isActive}\n");
        }

        public void ChangeActive()
        {
            if (isActive == true)
            {
                isActive = false;
                Console.WriteLine($"Player {Name} was baned!");
            }
            else
            {
                isActive = true;
                Console.WriteLine($"Player {Name} was unbaned!");
            }
        }
    }
}
