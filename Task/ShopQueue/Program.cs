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
            Queue<int> purchasesPrice = new Queue<int>();
            BuyAnything(purchasesPrice);

            while (purchasesPrice.Count > 0)
            {
                Console.Write($"Денег в кассе: {cashBalance}\n");
                cashBalance += SellAnything(purchasesPrice);
            }

            Console.WriteLine($"Денег в кассе: {cashBalance}\n");
        }
        static void BuyAnything(Queue<int> purchasesPrice)
        {
            Random random = new Random();
            int amountOurchases = 7;
            int minPrice = 20;
            int maxPrice = 700;

            for (int i = 0; i < amountOurchases; i++)
            {
                purchasesPrice.Enqueue(random.Next(minPrice, maxPrice));
            }            
        }
        static int SellAnything(Queue<int> purchasesPrice)
        {
            int cashBalance = 0;            
            Console.WriteLine($"Сумма текущей покупки: {purchasesPrice.Peek()}");
            cashBalance += purchasesPrice.Dequeue();
            Console.ReadKey(true);
            Console.Clear();
            return cashBalance;
        }        
    }
}
