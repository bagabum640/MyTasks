using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readint
{
    class Program
    {
        static void Main(string[] args)
        {     
            Console.WriteLine(ReadingText());                        
        }

        static int ReadingText ()
        {
            bool isNumber = false;
            string enteredText;
            int result = 0;

            while (isNumber != true)
            {
                Console.Write("Enter a number: ");
                enteredText = Console.ReadLine();
                isNumber = int.TryParse(enteredText, out result);                           
            }

            return result;
        }
    }
}
