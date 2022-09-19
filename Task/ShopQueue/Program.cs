using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            int cashBalance = 0;
            Queue<int> clients = new Queue<int>();
            FillTheQueue(clients);

            while (clients.Count > 0)
            {
                Console.Write($"Денег в кассе: {cashBalance}\n");
                cashBalance += ServeClient(clients);
            }

            Console.WriteLine($"Денег в кассе: {cashBalance}\n");
        }

        static void FillTheQueue(Queue<int> clients)
        {
            Random random = new Random();
            int amountPurchases = 7;
            int minPrice = 20;
            int maxPrice = 700;

            for (int i = 0; i < amountPurchases; i++)
            {
                clients.Enqueue(random.Next(minPrice, maxPrice));
            }            
        }

        static int ServeClient(Queue<int> clients)
        {
            int cashBalance = 0;            
            Console.WriteLine($"Сумма текущей покупки: {clients.Peek()}");
            cashBalance += clients.Dequeue();
            Console.ReadKey(true);
            Console.Clear();
            return cashBalance;
        }        
    }
}
