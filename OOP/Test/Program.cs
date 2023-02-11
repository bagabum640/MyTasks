using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            numbers.Add(2);
            numbers.Add(2);
            numbers.Add(2);           

            Console.WriteLine(numbers.Count);

            numbers.Clear();
            Console.WriteLine(numbers.Count);
        }
    }
}
