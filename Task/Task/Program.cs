using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIelement
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxHealth = 10;
            int currentHealth = maxHealth;
            int healthBarPosition = 0;
            ConsoleColor healthColor = ConsoleColor.Red;
            char healthChar = '#';

            int maxMana = 10;
            int currentMana = maxMana;
            int manaBarPosition = 1;
            ConsoleColor manaColor = ConsoleColor.Blue;
            char manaChar = '#';

            while (currentHealth > 0)
            {
                DrowBar(currentHealth, maxHealth, healthChar, healthColor, healthBarPosition);
                DrowBar(currentMana, maxMana, manaChar, manaColor, manaBarPosition);
                ChangeResource(ref currentHealth);
                ChangeResource(ref currentMana);
                Console.Clear();
            }
        }    

        static void DrowBar (int value, int maxValue, char charToDraw, ConsoleColor color, int position)
        {
            Console.SetCursorPosition(0, position);
            Console.Write('[');
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            for (int i = 0; i < value; i++)
            {
                Console.Write(charToDraw);
            }

            for (int i = value; i < maxValue; i++)
            {
                Console.Write(' ');
            }

            Console.ForegroundColor = defaultColor;
            Console.Write(']'+"\n");            
        }

        static void ChangeResource (ref int resource)
        {
            Console.Write("\nEnter value: ");
            int value = Convert.ToInt32(Console.ReadLine());
            resource += value;            
        }
    }
}
