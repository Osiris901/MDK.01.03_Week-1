using System;
using System.Collections.Generic;

namespace SGA_Task_05.Utils
{
    internal static class ConsoleUtils
    {
        public const int CursorOffsetX = 0;
        public const int CursorOffsetY = 3;

        public static void DrawMap(int[,] matrix, IReadOnlyDictionary<int, char> tileset)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (tileset.TryGetValue(matrix[x, y], out char symbol))
                    {
                        Console.CursorLeft = x + CursorOffsetX;
                        Console.CursorTop = y + CursorOffsetY;
                        Console.Write(symbol);
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                    
                }
            }
        }

        public static void DrawHealth(int playerHealth)
        {
            var bars = ((playerHealth / Constants.PlayerHealth) * 100) % 10;

            Console.Write("[");
            for (int i = 0; i < 10; i++)
            {
                if (i < bars)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write("_");
                }
            }

            Console.Write("] " + playerHealth + "/" + Constants.PlayerHealth);

        }

        public static void DrawInfo()
        {

        }

        public static void DrawUI()
        {

        }
    }
}
