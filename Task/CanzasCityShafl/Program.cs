using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanzasCityShafl
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            WriteArray(arr);
            Shuffle(arr);
            WriteArray(arr);
        }

        static void Shuffle(int [] enteredArray)
        {
            Random random = new Random();
            int buffer;
            int index;

            for (int i = 0; i < enteredArray.Length; i++)
            {
                index = random.Next(i, enteredArray.Length);                
                buffer = enteredArray[i];
                enteredArray[i] = enteredArray[index];
                enteredArray[index] = buffer;
            }
        }

        static void WriteArray(int [] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
