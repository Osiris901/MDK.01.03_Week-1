using System;
using System.IO;
using SGA_Task_05.Models;

namespace SGA_Task_05
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "level01.txt";
            Utils.CreateMap(path, out var matrix, out var enemies);

            var target = (x: 6, y: 9);
            var showPath = false;
            
            var map = new Map(matrix, enemies);
            var player = new Pawn("Player", Constants.PlayerHealth, Constants.PlayerAttack, 4, 0);

            var key = ConsoleKey.Q;
            while (key != ConsoleKey.X)
            {
                Utils.DrawUI(map, player, showPath);
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        MovePlayer(map, player, 0, -1);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        MovePlayer(map, player, -1, 0);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        MovePlayer(map, player, 1, 0);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        MovePlayer(map, player, 0, 1);
                        break;
                    case ConsoleKey.Spacebar:
                        Utils.MessageQueue += "Вы ждете...\n";
                        break;
                    case ConsoleKey.X:
                        continue;
                    case ConsoleKey.P:
                        if (showPath)
                        {
                            showPath = false;
                            Utils.MessageQueue += "Духи указывают вам путь.\n";
                        }
                        else
                        {
                            showPath = true;
                            Utils.MessageQueue += "Вы теряете связь с духами.\n";
                        }
                        break;
                    default:
                        Utils.MessageQueue += "Действие не распознано.\n";
                        continue;
                }

                if (player.X == target.x && player.Y == target.y)
                {
                    Utils.MessageQueue += "Вы прошли лабиринт!\n";
                    break;
                }

                if (player.Health <= 0)
                {
                    Utils.MessageQueue +=  "Ваше здоровье упало до 0.\n";
                    break;
                }
                
                // Enemy Turn
                map.MovePawns(player);
            }
            
            Utils.DrawUI(map, player, false);
            Console.WriteLine("Игра окончена!");
            Console.ReadKey();
        }

        private static void MovePlayer(Map map, Pawn player, int x, int y)
        {
            if (map.TryGetPawnAt(player.X + x, player.Y + y, out var enemy))
            {
                var isKilled = enemy.TakeDamage(player.Attack);
                Utils.MessageQueue += "Вы атакуете " + enemy.Name + ".\n";
                if (isKilled)
                {
                    map.ValidatePawns();
                    Utils.MessageQueue += "Вы убиваете " + enemy.Name + ".\n";
                }
            }
            else
            {
                var tile = map.GetTileAt(player.X + x, player.Y + y);
                if (tile == ' ')
                {
                    player.Move(x, y);
                }
                else
                {
                    Utils.MessageQueue += "В выбранном направлении нет прохода.\n";
                }
            }
        }
    }
}