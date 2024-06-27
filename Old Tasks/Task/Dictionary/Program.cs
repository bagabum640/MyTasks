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
                {"яблоко", "фрукт растущий на яблоне" },
                {"груша","можно кушать"},
                {"апельсин","солнца сын"} 
            };
            bool stopSearch = false;
            string enteredString;

            while (stopSearch != true)
            {
                Console.Write("Какой фрукт вы ищете: ");
                enteredString = Console.ReadLine().ToLower();

                if (enteredString != "exit")
                {
                    SearchInDictionary(enteredString, fruitDictionary);
                }
                else
                {
                    stopSearch = true;
                }                
            }
        }
        static void SearchInDictionary(string enteredString,  Dictionary<string, string> fruitDictionary)
        {
            if (fruitDictionary.ContainsKey(enteredString))
            {
                Console.WriteLine($"{enteredString} - это {fruitDictionary[enteredString]}\n");
                return;               
            }            
           
            Console.WriteLine("Такого фрукта нет!\n");
        }
    }
}
