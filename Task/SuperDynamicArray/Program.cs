using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDynamicArray
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> superArray  = new List<int>();            
            string enteredString;
            int enteredNumber;
            bool isExit = false;

            Console.WriteLine("Введите:\nЛюбое число - чтобы добавить его в массив\nshow - чтобы показать все числа в массиве\nsum - сложить все числа в массиве" +
                    "\nexit - чтобы выйти из программы\nremove - чтобы ввести число которое хотите удалить\nremoveAt - чтобы удалить число, указав его индекс\n");
            
            while (isExit != true)
            {                
                enteredString = Console.ReadLine().ToLower();

                if (int.TryParse(enteredString, out enteredNumber))
                {
                    superArray.Add(enteredNumber);
                }
                else
                {
                    switch (enteredString)
                    {
                        case "exit":
                            isExit = true;
                            break;

                        case "show":

                            foreach (var item in superArray)
                            {
                                Console.WriteLine(item);
                            }

                            break;

                        case "sum":
                            Console.WriteLine(superArray.Sum()); 
                            break;

                        case "remove":
                            Console.Write("Введите число, которое хотите удалить: ");
                            enteredString = Console.ReadLine();
                            Remove(enteredString, superArray);
                            break;

                        case "removeat":
                            Console.Write("Введите индекс числа, которое хотите удалить: ");
                            enteredString = Console.ReadLine();
                            RemoveAt(enteredString, superArray);
                            break;
                    }
                }               
            }  
        }

        static void RemoveAt(string enteredString, List<int> superArray)
        {
            int enteredNumber;

            if (int.TryParse(enteredString, out enteredNumber))
            {
                if (enteredNumber < superArray.Count)
                {
                    superArray.RemoveAt(enteredNumber);
                }
                else
                {
                    Console.WriteLine("Такого элемента нет!");
                }
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
            }
        }

        static void Remove (string enteredString, List<int> superArray)
        {
            int enteredNumber;

            if (int.TryParse(enteredString, out enteredNumber))
            {
                superArray.Remove(enteredNumber);
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
            }
        }
    }
}
