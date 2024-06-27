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
            const string BanPlayerCommand = "ban";
            const string UnbanPlayerCommand = "unban";
            const string DeletePlayerCommand = "delete";
            const string ExitCommand = "exit";

            string command;
            bool isExit = false;
            DataBase dataBase = new DataBase();

            Console.WriteLine($"Enter:\n{AddPlayerCommand} - to add new player\n{ShowPlayersCommand} - to show all players\n" +
                $"{BanPlayerCommand} - to ban player, used him id\n{UnbanPlayerCommand} - to unban player, used him id\n" +
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

                    case BanPlayerCommand:
                        dataBase.BanPlayer();
                        break;

                    case UnbanPlayerCommand:
                        dataBase.UnbanPlayer();
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

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
            }            
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
            }            
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
            }                     
        }

        private bool TryGetPlayer (out Player player)
        {
            Console.Write("Enter id: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                foreach (var _player in _players)
                {
                    if (_player.ID == id)
                    {
                        player = _player;
                        return true;
                    }                    
                }

                player = null;
                Console.WriteLine("Wrong id!");
                return false;
            }
            else
            {
                player = null;
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
        public bool IsActive { get; private set; }

        public Player (int id, string name)
        {
            ID = id;
            Name = name;
            Level = 1;
            IsActive = true;
        }

        public void ShowStats()
        {
            Console.WriteLine($"ID: {ID}\nName: {Name}\nLevel:{Level}\nActive: {IsActive}\n");
        }

        public void Ban()
        {
            if (IsActive == true)
            {
                IsActive = false;
                Console.WriteLine($"Player {Name} was baned!");
            }
            else
            {                
                Console.WriteLine($"The player {Name} has already been banned!");
            }
        }

        public void Unban()
        {
            if (IsActive == false)
            {
                IsActive = true;
                Console.WriteLine($"Player {Name} was unbaned!");
            }
            else
            {                
                Console.WriteLine($"The player {Name} has not ban!");
            }
        }
    }
}
