using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            const string SellerMenuCommand = "да";
            const string PlayerMenuCommand = "меню";
            const string ExitCommand = "нет";

            bool isExit = false;
            string command;
            Seller seller = new Seller();
            Player player = new Player();

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine($"Добро пожаловать в наш магазин! Желаете взглянуть на товар?\n" +
                $"\n{SellerMenuCommand} - посмотреть товары\n{PlayerMenuCommand} - открыть инвентарь\n{ExitCommand} - покинуть магазин\n");
                command = Console.ReadLine();

                switch (command)
                {
                    case SellerMenuCommand:
                        seller.CallMenu(player);
                        break;

                    case PlayerMenuCommand:
                        player.CallMenu();
                        break;

                    case ExitCommand:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Не понял, повторите, что вы хотите?");
                        break;
                }
            }
        }
    }

    class Item
    {
        public string Name { get; private set; }
        public uint Cost { get; private set; }

        public Item(string name, uint cost)
        {
            Name = name;
            Cost = cost;
        }

        public void Show()
        {
            Console.WriteLine($"{Name} - стоимость {Cost} монет");
        }
    }

    class Inventory : IComparer<Item>
    {
        private List<Item> _inventory = new List<Item>();
        public uint Golds { get; private set; }

        public void AddSomeItems(Item[] items)
        {
            _inventory.AddRange(items);
        }

        public void ShowItems()
        {
            foreach (var item in _inventory)
            {
                item.Show();
            }            
        }

        public void TakeGold(uint golds)
        {
            Golds += golds;
        }

        public void GiveGold(uint golds)
        {
            Golds -= golds;
        }

        public bool TryFindItem(out Item item, string itemName)
        {
            foreach (var _item in _inventory)
            {
                if (_item.Name == itemName)
                {
                    item = _item;
                    return true;
                }
            }

            item = null;
            return false;
        }

        public void DeleteItem(Item item)
        {
            _inventory.Remove(item);
        }

        public void AddItem(Item item)
        {
            _inventory.Add(item);
        }

        public void Sort()
        {
            _inventory.Sort(Compare);
        }

        public int Compare(Item item1, Item item2)
        {
            string itemName1 = item1.Name.ToString();
            string itemName2 = item2.Name.ToString();

            return String.Compare(itemName1, itemName2);
        }
    }

    class Seller
    {
        private Inventory _inventory = new Inventory();

        public Seller()
        {
            Item[] items =
            {
                new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60),
                new Item("Вода", 15), new Item("Вода", 15), new Item("Вода", 15),
                new Item("Хлеб", 30), new Item("Хлеб", 30), new Item("Хлеб", 30), new Item("Хлеб", 30),
                new Item("Колбаса", 80), new Item("Колбаса", 80), new Item("Сыр", 30), new Item("Сыр", 30),
            };

            _inventory.AddSomeItems(items);
            _inventory.TakeGold(1000);
        }

        public void CallMenu(Player player)
        {
            const string ExitCommand = "выйти";

            bool isExit = false;
            string command;

            while (isExit != true)
            {
                Console.Clear();
                Console.WriteLine("Выбирай!\n");
                _inventory.Sort();
                _inventory.ShowItems();
                Console.WriteLine($"\nЗолото у торговца: {_inventory.Golds}.\tВаше золото: {player.ShowBalance()}");
                Console.WriteLine($"\nВведите название товара, чтобы купить его, \n{ExitCommand} - чтобы закончить покупки.\n\nКупить: ");
                command = Console.ReadLine();

                if (command == ExitCommand)
                {
                    isExit = true;
                }
                else
                {
                    Sell(command, player);
                }
            }
        }

        private void Sell(string itemName, Player player)
        {
            if (_inventory.TryFindItem(out Item item, itemName))
            {
                if (player.ShowBalance() >= item.Cost)
                {
                    player.BuyItem(item);
                    _inventory.TakeGold(item.Cost);
                    _inventory.DeleteItem(item);
                }
                else
                {
                    Console.WriteLine("Недостаточно золота.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"{itemName} отсутствует в продаже.");
                Console.ReadKey();
            }
        }
    }

    class Player
    {
        private Inventory _inventory = new Inventory();

        public Player()
        {
            _inventory.TakeGold(200);
        }

        public void BuyItem(Item item)
        {
            _inventory.GiveGold(item.Cost); 
            _inventory.AddItem(item);
        }

        public uint ShowBalance()
        {
            return _inventory.Golds;
        }

        public void CallMenu()
        {
            const string ExitCommand = "выйти";

            bool isExit = false;
            string command;

            while (isExit != true)
            {
                Console.Clear();
                Console.WriteLine("Предметы в инвентаре:\n");
                _inventory.Sort();
                _inventory.ShowItems();
                Console.WriteLine("\nВаше золото: " + ShowBalance());
                Console.WriteLine($"\nВведите название предмета, чтобы использовать его, \n{ExitCommand} - чтобы закрыть инвентарь.\n\nИспользовать: ");
                command = Console.ReadLine();

                if (command == ExitCommand)
                {
                    isExit = true;
                }
                else
                {
                    UseItem(command);
                }
            }
        }

        private void UseItem(string itemName)
        {
            if (_inventory.TryFindItem(out Item item, itemName))
            {
                Console.WriteLine($"\nСъедено {item.Name}. Нажмите любую клавишу для продолжения.");
                _inventory.DeleteItem(item);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"{itemName} отсутствует в инвентаре.");
                Console.ReadKey();
            }
        }
    }
}
