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
            List<int> numbers  = new List<int>();            
            string enteredString;
            int enteredNumber;
            bool isExit = false;
            const string CommandExit = "exit";
            const string CommandShowNumbers = "show";
            const string CommandSum = "sum";
            const string CommandRemove = "remove";
            const string CommandRemoveAt = "removeat";

            Console.WriteLine($"Введите:\nЛюбое число - чтобы добавить его в массив\n{CommandShowNumbers} - чтобы показать все числа в массиве\n{CommandSum} - сложить все числа в массиве" +
                    $"\n{CommandExit} - чтобы выйти из программы\n{CommandRemove} - чтобы ввести число которое хотите удалить\n{CommandRemoveAt} - чтобы удалить число, указав его индекс\n");
            
            while (isExit != true)
            {                
                enteredString = Console.ReadLine().ToLower();

                if (int.TryParse(enteredString, out enteredNumber))
                {
                    numbers.Add(enteredNumber);
                }
                else
                {
                    switch (enteredString)
                    {
                        case CommandExit:
                            isExit = true;
                            break;

                        case CommandShowNumbers:

                            foreach (var number in numbers)
                            {
                                Console.WriteLine(number);
                            }

                            break;

                        case CommandSum:
                            Console.WriteLine(numbers.Sum()); 
                            break;

                        case CommandRemove:
                            Console.Write("Введите число, которое хотите удалить: ");
                            enteredString = Console.ReadLine();
                            Remove(enteredString, numbers);
                            break;

                        case CommandRemoveAt:
                            Console.Write("Введите индекс числа, которое хотите удалить: ");
                            enteredString = Console.ReadLine();
                            RemoveAt(enteredString, numbers);
                            break;
                    }
                }               
            }  
        }

        static void RemoveAt(string enteredString, List<int> numbers)
        {            

            if (int.TryParse(enteredString, out int enteredNumber))
            {
                if (enteredNumber < numbers.Count)
                {
                    numbers.RemoveAt(enteredNumber);
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

        static void Remove (string enteredString, List<int> numbers)
        {            

            if (int.TryParse(enteredString, out int enteredNumber))
            {
                numbers.Remove(enteredNumber);
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
            }
        }
    }
}
