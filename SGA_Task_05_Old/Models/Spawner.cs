using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class Spawner : MapObject 
    {
        private readonly Enemy _prototype;

        public Spawner(Point position, Enemy prototype) : base(position)
        {
            _prototype = prototype;
        }

        public Enemy Spawn()
        {
            return _prototype.Clone() as Enemy;
        }
    }
}
