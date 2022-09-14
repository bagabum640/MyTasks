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
                SellAnything(ref cashBalance, purchasesPrice);
            }

            Console.WriteLine($"Денег в кассе: {cashBalance}\n");
        }
        static void BuyAnything(Queue<int> purchasesPrice)
        {
            Random random = new Random();

            for (int i = 0; i < 7; i++)
            {
                purchasesPrice.Enqueue(random.Next(20, 700));
            }            
        }
        static void SellAnything(ref int cashBalance, Queue<int> purchasesPrice)
        {
            Console.Write($"Денег в кассе: {cashBalance}\n");
            Console.WriteLine($"Сумма текущей покупки: {purchasesPrice.Peek()}");
            cashBalance += purchasesPrice.Dequeue();
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
