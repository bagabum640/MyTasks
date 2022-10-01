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
            Player player = new Player (5, 5, '@');
            Renderer renderer = new Renderer();
            renderer.DrawPlayer(player);
        }
    }

    class Player
    {
        private int _positionX;
        private int _positionY;
        public char PlayerModel;

        public Player (int positionX, int positionY, char playerModel)
        {
            _positionX = positionX;
            _positionY = positionY;
            PlayerModel = playerModel;
        }        

        public int PositionX { get { return _positionX; } private set { ; } }
        public int PositionY { get { return _positionY; } private set { ; } }
    }

    class Renderer
    {       
        public void DrawPlayer (Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.WriteLine(player.PlayerModel);
        }
    }
}
