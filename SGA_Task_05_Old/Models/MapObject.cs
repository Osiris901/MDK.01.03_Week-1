using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class MapObject
    {
        public Point Position { get; protected set; }

        public MapObject(Point position)
        {
            Position = position;
        }

        public MapObject(int x, int y)
        {
            Position = new Point(x, y);
        }
    }
}
