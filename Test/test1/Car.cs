using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace test1
{
    class Car
    {
        public string name;
        public int speed;

        public void Representation()
        {
            Console.WriteLine($"{name}: {speed}.");
            Console.WriteLine(name);            
        }

        public Car()
        {
               
        }
    }
}
