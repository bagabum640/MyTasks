﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();

            supermarket.ServingCustomers();
        }
    }

    class Supermarket
    {
        private int _money;
        private int _customersNumber;
        private Random _random = new Random();
        private Queue<Customer> _customers = new Queue<Customer>();
        private Product[] _products =
            {
                new Product("Мыло", 20), new Product("Веревка", 35), new Product("Водка", 70), new Product("Пиво", 50),
                new Product("Макароны", 60), new Product("Рис", 65), new Product("Вода", 30), new Product("Капуста", 40),
            };

        public Supermarket()
        {
            _customersNumber = _random.Next(10, 13);

            for (int i = 0; i < _customersNumber; i++)
            {
                _customers.Enqueue(new Customer(_random.Next(120, 200), GenerateProductBasket()));
            }
        }

        public void ServingCustomers()
        {
            while (_customers.Count > 0)
            {
                Console.WriteLine($"Денег в кассе: {_money}.\t\t Клиентов в очереди: {_customersNumber}.");
                ServeCustomer(_customers.Dequeue());
            }

            Console.WriteLine("Все клиенты обслужены. Магазин закрывается.");
        }

        private void ServeCustomer(Customer customer)
        {
            Console.WriteLine("\nКлиент подходит к кассе.\n");
            _money += customer.TryBuyProducts();
            Console.WriteLine("\nКлиент покидает магазин.");
            _customersNumber--;
            Console.ReadKey();
            Console.Clear();
        }

        private List<Product> GenerateProductBasket()
        {          
            List<Product> productbasket = new List<Product>();

            for (int i = 0; i < _random.Next(1, _products.Length); i++)
            {
                productbasket.Add(_products[_random.Next(_products.Length)]);
            }

            return productbasket;
        }
    }

    class Customer
    {
        private int _money;
        private int _basketPrice;
        private List<Product> _basket = new List<Product>();

        public Customer(int money, List<Product> products)
        {
            _money = money;
            _basket.AddRange(products);
        }

        public int TryBuyProducts()
        {
            CalculateBasketPrice();

            if (_money >= _basketPrice)
            {
                BuyProduct();                
            }
            else
            {
                LayOutRandomProduct();
                TryBuyProducts();
            }

            return _basketPrice;
        }

        private void CalculateBasketPrice()
        {
            _basketPrice = 0;
            Console.WriteLine("В корзине покупателя:");

            foreach (var product in _basket)
            {
                _basketPrice += product.Price;
                product.ShowInfo();
            }

            Console.WriteLine($"Итоговая сумма: {_basketPrice} рублей.");
            Console.ReadKey();
        }

        private void BuyProduct()
        {
            _money -= _basketPrice;
            _basket.Clear();
            Console.WriteLine("Покупатель достает кошелек и оплачивает товар.");
        }

        private void LayOutRandomProduct()
        {
            Random random = new Random();
            int productIndex = random.Next(_basket.Count);

            Console.WriteLine($"\nУ клиента недостаточно денег. {_basket[productIndex].Name} покидает его корзину и покупатель горько вздыхает.\n");
            _basket.RemoveAt(productIndex);
        }
    }

    class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} - {Price} рублей.");
        }
    }
}
