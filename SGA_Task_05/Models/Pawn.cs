using System;

namespace SGA_Task_05.Models
{
    public class Pawn
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; private set; }
        public int Attack { get; protected set; }

        public Pawn(string name, int maxHealth, int attack, int startX, int startY)
        {
            Name = name;
            Health = maxHealth;
            MaxHealth = maxHealth;
            Attack = attack;
            X = startX;
            Y = startY;
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

        public bool TakeDamage(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            Health -= amount;
            return Health <= 0;
        }
    }
}