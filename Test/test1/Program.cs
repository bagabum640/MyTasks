using System;
using System.Text;
using System.Linq;


namespace test1
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Person p = new Person();
            
           // Console.WriteLine(Person.str);
        }                
    }     
    
    class Person
    {
        public static int str;

       public Person()
        {
            Console.WriteLine("2");
        }

        static Person()
        {
            str = 1;
            Console.WriteLine("0");
        }
    }

    
}
