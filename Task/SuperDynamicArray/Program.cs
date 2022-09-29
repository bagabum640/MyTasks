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
            const string CommandExit = "exit";
            const string CommandShowNumbers = "show";
            const string CommandSum = "sum";
            const string CommandRemove = "remove";
            const string CommandRemoveAt = "removeat";
            List<int> numbers = new List<int>();
            string enteredString;            
            bool isExit = false;

            Console.WriteLine($"Введите:\nЛюбое число - чтобы добавить его в массив\n{CommandShowNumbers} - чтобы показать все числа в массиве\n{CommandSum} - сложить все числа в массиве" +
                    $"\n{CommandRemoveAt} - чтобы удалить число, указав его индекс\n{CommandRemove} - чтобы ввести число которое хотите удалить" +
                    $"\n{CommandExit} - чтобы выйти из программы\n");

            while (isExit != true)
            {
                enteredString = Console.ReadLine().ToLower();

                switch (enteredString)
                {
                    case CommandExit:
                        isExit = true;
                        break;

                    case CommandShowNumbers:
                        ShowNumbers(numbers);
                        break;

                    case CommandSum:
                        Console.WriteLine(numbers.Sum());
                        break;

                    case CommandRemove:                        
                        Remove(numbers);
                        break;

                    case CommandRemoveAt:                        
                        RemoveAt(numbers);
                        break;

                    default:
                        AddNumber(enteredString, numbers);
                        break;
                }
            }
        }

        static void RemoveAt(List<int> numbers)
        {
            Console.Write("Введите индекс числа, которое хотите удалить: ");
            string enteredString = Console.ReadLine();

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

        static void Remove(List<int> numbers)
        {
            Console.Write("Введите число, которое хотите удалить: ");
            string enteredString = Console.ReadLine();

            if (int.TryParse(enteredString, out int enteredNumber))
            {
                numbers.Remove(enteredNumber);
            }
            else
            {
                Console.WriteLine("Вы ввели не число!");
            }
        }

        static void AddNumber(string enteredString, List<int> numbers)
        {
            if (int.TryParse(enteredString, out int enteredNumber))
            {
                numbers.Add(enteredNumber);
            }
            else
            {
                Console.WriteLine("Введенная строка не соответствует ни одной команде и не является числом!");
            }
        }

        static void ShowNumbers(List<int> numbers)
        {
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
