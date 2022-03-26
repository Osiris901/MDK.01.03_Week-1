using System;
using System.Collections.Generic;
using System.Linq;

namespace SGA_Task_05.Models
{
    public class Map
    {
        private char[,] m_matrix;
        private List<Pawn> m_enemies;
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Map(char[,] matrix, IEnumerable<Pawn> enemies)
        {
            m_matrix = matrix;
            m_enemies = new List<Pawn>(enemies);

            Width = m_matrix.GetLength(0);
            Height = m_matrix.GetLength(1);
        }

        public char GetTileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return '╬';

            return m_matrix[x, y];
        }

        public bool TryGetPawnAt(int x, int y, out Pawn pawn)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                pawn = null;
                return false;
            }

            pawn = m_enemies.FirstOrDefault(p => p.X == x && p.Y == y);

            return pawn != null;
        }

        public void Draw()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Console.CursorLeft = x;
                    Console.CursorTop = y;
                    Console.Write(m_matrix[x, y]);
                }
            }

            
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = Constants.EnemyColor; 
            foreach (var enemy in m_enemies)
            {
                Utils.DrawAt('*', enemy.X, enemy.Y);
            }
            Console.ForegroundColor = temp;
            
            temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green; 
            Utils.DrawAt('·', 6, 9);
            Console.ForegroundColor = temp;
        }

        public void ValidatePawns()
        {
            var pawns = m_enemies.ToArray();
            foreach (var pawn in pawns)
            {
                if (pawn.Health <= 0)
                {
                    m_enemies.Remove(pawn);
                }
            }
        }

        public void MovePawns(Pawn player)
        {
            var rnd = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            foreach (var enemy in m_enemies)
            {
                var x = (rnd.Next(10) % 3) - 1;
                var y = 0;
                if (x == 0)
                {
                    y = (rnd.Next(10) % 3) - 1;
                }
                
                if (player.X == enemy.X + x && player.Y == enemy.Y + y)
                {
                    player.TakeDamage(enemy.Attack);
                    Utils.MessageQueue += enemy.Name + " атакует вас.\n";
                }
                else
                {
                    var tile = GetTileAt(enemy.X + x, enemy.Y + y);
                    if (tile == ' ' && (enemy.X + x != 6 && enemy.Y + y != 9))
                    {
                        enemy.Move(x, y);
                    }
                }
            }
        }
    }
}