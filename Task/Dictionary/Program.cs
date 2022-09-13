using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> fruitDictionary = new Dictionary<string, string>() 
            {
                {"Яблоко", "фрукт растущий на яблоне" },
                {"Груша","можно кушать"},
                {"Апельсин","солнца сын"} 
            };

            while (true)
            {
                SearchInDictionary(fruitDictionary);
            }
        }
        static void SearchInDictionary(Dictionary<string, string> fruitDictionary)
        {
            string enteredString;

            Console.Write("Какой фрукт вы ищете: ");
            enteredString = Console.ReadLine();

            foreach (var item in fruitDictionary)
            {
                if (item.Key.ToLower() == enteredString.ToLower())
                {
                    Console.WriteLine($"{item.Key} - это {item.Value}\n");
                    return;
                }
            }
           
            Console.WriteLine("Такого фрукта нет!\n");
        }
    }
}
