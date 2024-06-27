using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithProperty
{
    class Program
    {
        static void Main(string[] args)
        {
            int positionX = 5;
            int positionY = 5;
            char playerSymbol = '@';

            Player player = new Player (positionX, positionY, playerSymbol);
            Renderer renderer = new Renderer();
            renderer.DrawPlayer(player);
            Console.WriteLine();
        }
    }

    class Player
    {
        public char Model { get; private set; }
        public int PositionX { get ; private set; }
        public int PositionY { get ; private set; }

        public Player (int positionX, int positionY, char model)
        {
            PositionX = positionX;
            PositionY = positionY;
            Model = model;
        }   
    }

    class Renderer
    {       
        public void DrawPlayer (Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Model);
        }
    }
}
