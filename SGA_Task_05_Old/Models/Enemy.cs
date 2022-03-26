using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class Enemy : Entity
    {
        public Enemy(string name, Point position, int startHealth, int attack) : base(name, position, startHealth, attack)
        {
        }
    }
}
