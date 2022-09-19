using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergingCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstString = { "1", "3", "4", "3", "5"};
            string[] secondString = { "4", "6", "5", "8", "9" };
            List<string> mergedStrings = new List<string>();

            AddNewElements(mergedStrings, firstString);
            AddNewElements(mergedStrings, secondString);

            foreach (var item in mergedStrings)
            {
                Console.WriteLine(item);
            }
        }

        static void AddNewElements (List<string> mergedStrings, string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (mergedStrings.Contains(array[i]) != true)
                {
                    mergedStrings.Add(array[i]);
                }                
            }
        }
    }
}
