using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class Player : Entity
    {
        public Player(Point position, int startHealth, int attack) : base("Player", position, startHealth, attack)
        {
        }
    }
}
