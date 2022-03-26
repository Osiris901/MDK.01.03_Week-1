using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SGA_Task_05.Models;

namespace SGA_Task_05
{
    public static class Utils
    {
        public static string MessageQueue = "";
        
        public static void CreateMap(string path, out char[,] matrix, out List<Pawn> enemies)
        {
            var lines = new List<string>();
            using (var sr = new StreamReader(path))
            {
                while (sr.EndOfStream == false)
                {
                    lines.Add(sr.ReadLine());
                }
            }

            var width = lines[0].Length;
            var height = lines.Count;
            if (lines.Any(l => l.Length != width) || width != height)
            {
                throw new ArgumentException();
            }

            matrix = new char[width, height];
            enemies = new List<Pawn>();
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var ch = lines[y][x];
                    if (ch == '*')
                    {
                        matrix[x, y] = ' ';
                        enemies.Add(new Pawn("Slime", Constants.EnemyHealth, Constants.EnemyAttack, x, y));
                    }
                    else
                    {
                        matrix[x, y] = ch;
                    }
                }
            }
        }

        private static void DrawStatus(Pawn player)
        {
            var bars = 10;
            var fill = (int)( player.Health / (player.MaxHealth / 100M));
            if (player.Health != player.MaxHealth)
            {
                bars = (int)(fill / 10f);
            }
            Console.Write("Hp: [");
            for (int i = 0; i < 10; i++)
            {
                if (i <= bars)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write('_');
                }
            }
            Console.WriteLine("]");
        }

        private static void DrawPlayer(Pawn player)
        {
            var t = Console.ForegroundColor;
            Console.ForegroundColor = Constants.PlayerColor;
            
            DrawAt('■', player.X, player.Y);
            
            Console.ForegroundColor = t;
        }

        private static void DrawPath()
        {
            var t = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            
            DrawAt('·', 4, 0);
            DrawAt('·', 4, 1);
            DrawAt('·', 4, 2);
            DrawAt('·', 4, 3);
            DrawAt('·', 4, 4);
            DrawAt('·', 3, 4);
            DrawAt('·', 2, 4);
            DrawAt('·', 2, 5);
            DrawAt('·', 2, 6);
            DrawAt('·', 3, 6);
            DrawAt('·', 4, 6);
            DrawAt('·', 5, 6);
            DrawAt('·', 6, 6);
            DrawAt('·', 6, 5);
            DrawAt('·', 6, 4);
            DrawAt('·', 6, 3);
            DrawAt('·', 6, 2);
            DrawAt('·', 6, 1);
            DrawAt('·', 7, 1);
            DrawAt('·', 8, 1);
            DrawAt('·', 8, 2);
            DrawAt('·', 8, 3);
            DrawAt('·', 8, 4);
            DrawAt('·', 8, 5);
            DrawAt('·', 8, 6);
            DrawAt('·', 8, 7);
            DrawAt('·', 8, 8);
            DrawAt('·', 7, 8);
            DrawAt('·', 6, 8);
            
            Console.ForegroundColor = t;
        }
        
        public static void DrawUI(Map map, Pawn player, bool path)
        {
            Console.CursorVisible = false;
            Console.Clear();
            if (path)
            {
                DrawPath();
            }
            map.Draw();
            DrawPlayer(player);
            Console.WriteLine();
            DrawStatus(player);
            Console.Write(MessageQueue);
            MessageQueue = "";
        }

        public static void DrawAt(char ch, int x, int y)
        {
            var (cursorTop, cursorLeft) = (Console.CursorTop, Console.CursorLeft);
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(ch);
            Console.CursorTop = cursorTop;
            Console.CursorLeft = cursorLeft;
        }
    }
}