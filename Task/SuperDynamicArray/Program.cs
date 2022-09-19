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
            string enteredCommand;
            int enteredNumber;
            bool isExit = false;            

            while (isExit != true)
            {
                enteredCommand = Console.ReadLine().ToLower();

                if (int.TryParse(enteredCommand, out enteredNumber))
                {
                    superArray.Add(enteredNumber);
                }
                else
                {
                    switch (enteredCommand)
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
                    }
                }
            }            
        }        
    }
}
