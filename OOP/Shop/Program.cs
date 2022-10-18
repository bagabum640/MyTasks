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
            
                        
            Console.WriteLine($"Добро пожаловать в наш магазин! Желаете взглянуть на товар?\n" +
                $"\n{SellerMenuCommand} - посмотреть товары\n{PlayerMenuCommand} - открыть инвентарь\n{ExitCommand} - покинуть магазин");

            while (isExit == false)
            {
                Console.WriteLine();
                command = Console.ReadLine();

                switch (command)
                {
                    case SellerMenuCommand:
                        seller.CallMenu();
                        break;

                    case PlayerMenuCommand:
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
        public int Cost { get; private set; }

        public Item(string name, int cost)
        {
            Name = name;
            Cost = cost;
        }

        public void Show()
        {
            Console.WriteLine($"{Name} - стоит {Cost} монет");
        }
    }

    class Seller
    {
        private List<Item> _inventory = new List<Item>();

        public Seller()
        {
            Item[] items = { 
                new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60), new Item("Пироженое", 60),
                new Item("Вода", 15), new Item("Вода", 15), new Item("Вода", 15),
                new Item("Хлеб", 30), new Item("Хлеб", 30), new Item("Хлеб", 30), new Item("Хлеб", 30),
                new Item("Колбаса", 80), new Item("Колбаса", 80), new Item("Сыр", 30), new Item("Сыр", 30),
            };
            _inventory.AddRange(items);
        }

        public void CallMenu()
        {
            

            Console.Clear();
            Console.WriteLine("Выбирай!\n");
            ShowItems();
            Console.Write("\nКупить: ");
            

            
        }

        private void ShowItems()
        {
            foreach (var item in _inventory)
            {
                item.Show();
            }
        }

        private void Sell()
        {
            string itemName;
            itemName = Console.ReadLine();

            foreach (var item in _inventory)
            {
                if (item.Name == itemName)
                {

                }
            }
        }        
    }
}
