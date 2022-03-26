using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal readonly struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Distance(Point b)
        {
            return (int)Math.Ceiling(Math.Sqrt((X - b.X) ^ 2 + (Y - b.Y) ^ 2));
        }

        public static Point operator +(Point a) => a;
        public static Point operator -(Point a) => new Point(-a.X, -a.Y);
        public static Point operator ++(Point a) => new Point(a.X + 1, a.Y + 1);
        public static Point operator --(Point a) => new Point(a.X - 1, a.Y - 1);

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
        public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);

        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Point a, Point b) => a.X != b.X && a.Y != b.Y;

        public static Point Zero = new Point(0, 0);
        public static Point Up = new Point(0, -1);
        public static Point Down = new Point(0, 1);
        public static Point Left = new Point(-1, 0);
        public static Point Right = new Point(1, 0);

        public override bool Equals(object obj)
        {
            return obj is Point p && this == p;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "Point(" + X + ", " + Y + ")";
        }
    }
}
