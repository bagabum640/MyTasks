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
            List<int> numbers = new List<int>(1);
            numbers.Add(2);
            numbers[0] = 3;


            Console.WriteLine(numbers[0]);
        }
    }
}
