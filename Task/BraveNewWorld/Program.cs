using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;
            int quantityDots = 0;
            int collectedDots = 0;
            int positionX, positionY;
            int directionX = 0, directionY = 1;
            char[,] map = ReadMap("Map.txt", out positionX, out positionY, ref quantityDots);
            
            Console.CursorVisible = false;
            DrowMap(map);

            while (isPlaying)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    ChangeDirection(key, ref directionX, ref directionY);
                }

                if (map[positionX + directionX, positionY + directionY] != '#')
                {
                    Move(ref positionX, ref positionY, directionX, directionY);                    
                }

                CollectDots(map, positionX, positionY, ref collectedDots, quantityDots, ref isPlaying);
                ShowCounter(quantityDots, collectedDots);
                System.Threading.Thread.Sleep(150);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;            
            Console.WriteLine("Congratulation! You win!");
        }

        static void ChangeDirection(ConsoleKeyInfo key, ref int directionX, ref int directionY)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;
                case ConsoleKey.UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;
                case ConsoleKey.RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;
                case ConsoleKey.DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;
            }
        }

        static void CollectDots (char[,] map, int positionX, int positionY, ref int collectedDots, int quantityDots, ref bool isPlaying)
        {
            if (map [positionX, positionY] == '.')
            {
                map[positionX, positionY] = ' ';
                collectedDots++;
            }

            if (collectedDots == quantityDots)
            {
                isPlaying = false;
            }
        }

        static void ShowCounter(int quantityDots, int collectedDots)
        {
            Console.SetCursorPosition(1, 11);
            Console.Write($"{collectedDots}/{quantityDots}");
        }

        static void Move(ref int positionX, ref int positionY, int directionX, int directionY)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(' ');

            positionX += directionX;
            positionY += directionY;

            Console.SetCursorPosition(positionY, positionX);
            Console.Write('@');
        }

        static void DrowMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static char[,] ReadMap(string mapName, out int positionX, out int positionY, ref int quantityDots)
        {
            positionX = 0;
            positionY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        positionX = i;
                        positionY = j;
                    }

                    if (map[i, j] == '.')
                    {
                        quantityDots++;
                    }
                }
            }

            return map;
        }
    }
}
