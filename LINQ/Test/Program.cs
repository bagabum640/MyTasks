using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> first = new List<int>();
            List<int> second = new List<int>();

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] numbers2 = { 1, 2, 3, 8, 9 };

            first.AddRange(numbers);
            second.AddRange(numbers2);

            foreach (var number in first)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();

            foreach (var number in numbers2)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();

            foreach (var number in first.Except(second).ToList())
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();

            foreach (var number in first)
            {
                Console.WriteLine(number);
            }
        }
    }
}