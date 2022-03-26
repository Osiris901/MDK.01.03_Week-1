using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA_Task_05.Models;

namespace SGA_Task_05
{
    internal class Program
    {
        private static Map _map;
        private static int _tick = 0;
        private static Random _rnd;

        private static string _infoBuffer = "";

        static void Main(string[] args)
        {
            var levelFile = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "level01.txt";
            _map = Map.FromFile(levelFile);

            _rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);

            var player = _map.GetObjectsByType<Player>().First();
            var flag = _map.GetObjectsByType<Flag>().First();

            var command = GameCommand.None;

            while (command != GameCommand.Exit)
            {
                Draw();
                command = GetKey();

                switch (command)
                {
                    case GameCommand.Exit:
                        Console.WriteLine("Программа завершена. Нажмите любую клавишу чтобы закрыть это окно...");
                        Console.ReadKey();
                        break;
                    case GameCommand.MoveUp:
                        player.Move(Point.Up);
                        break;
                    case GameCommand.MoveDown:
                        player.Move(Point.Down);
                        break;
                    case GameCommand.MoveLeft:
                        player.Move(Point.Left);
                        break;
                    case GameCommand.MoveRight:
                        player.Move(Point.Right);
                        break;
                    default:
                        continue;
                }

                if (player.Position == flag.Position)
                {
                    Console.WriteLine("Поздравляем, Вы нашли выход!");
                    command = GameCommand.Exit;
                    continue;
                }

                ActivateEnemies(player);

                if (player.Health <= 0)
                {
                    Console.WriteLine("К сожалению вы не смогли выбраться из лабиринта.");
                    command = GameCommand.Exit;
                    continue;
                }

                _map.Invalidate();
                _tick++;
            }
        }

        private static GameCommand GetKey()
        {
            var key = Console.ReadKey(false);
            switch (key.Key)
            {
                case ConsoleKey.W:
                    return GameCommand.MoveUp;
                case ConsoleKey.S:
                    return GameCommand.MoveDown;
                case ConsoleKey.A:
                    return GameCommand.MoveLeft;
                case ConsoleKey.D:
                    return GameCommand.MoveRight;
                case ConsoleKey.Spacebar:
                    return GameCommand.Wait;
                case ConsoleKey.X:
                    return GameCommand.Exit;
                case ConsoleKey.M:
                    return GameCommand.Cheat;
                default:
                    return GameCommand.None;
            }
        }

        private static void Draw()
        {
            // Drawing labyrinth

            // Draw Status bar

            // Print buffer && clear
        }

        private static void ActivateEnemies(Player player)
        {
            var enemies = _map.GetObjectsByType<Enemy>();
            var directions = new []{Point.Up, Point.Down, Point.Left, Point.Right};

            foreach (var enemy in enemies)
            {
                var d = enemy.Position.Distance(player.Position);
                var rnd = _rnd.Next(100);

                if (d == 1 && rnd % 5 > 1)
                {
                    player.TakeDamage(enemy.Attack);
                    _infoBuffer += enemy.Name + " атакует вас, нанося " + enemy.Attack + " урона.\n";
                    continue;
                }

                rnd = _rnd.Next(directions.Length);
                var target = _map.GetObjectAt(directions[rnd] + enemy.Position);
                if (target == null || target is Wall || target is Entity)
                {
                    continue;
                }

                enemy.Move(directions[rnd]);
            }
        }

        private static void TryMovePlayer(Player player, Point direction)
        {
            var target = _map.GetObjectAt(player.Position + direction);
            if (target == null || target is Wall)
            {
                return;
            }

            if (target is Entity entity)
            {
                var isKilled = entity.TakeDamage(player.Attack);
                _infoBuffer = "Вы атакуете " + entity.Name + ", нанося " + player.Attack + "урона.\n";
                if (isKilled)
                {
                    _infoBuffer += entity.Name + " погибает!";
                }
            }
            else
            {
                player.Move(direction);
            }
        }
    }
}
