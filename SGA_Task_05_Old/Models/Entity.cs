using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class Entity : MapObject, ICloneable
    {
        public static int counter = 0;

        public int Id { get; }

        public string Name { get; }
        public int MaxHealth { get; }
        public int Health { get; protected set; }
        public int Attack { get; protected set; }

        public Entity(string name, Point position, int startHealth, int attack) : base(position)
        {
            Id = counter++;
            Name = name;

            MaxHealth = startHealth;
            Health = startHealth;

            Attack = attack;
        }

        public void Move(Point direction)
        {
            Position += direction;
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;

            return Health < 0;
        }

        public object Clone()
        {
            return new Entity(Name, Position, MaxHealth, Attack);
        }
    }
}
