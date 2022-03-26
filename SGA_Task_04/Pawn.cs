using System;

namespace SGA_Task_04
{
    internal class Pawn
    {
        public readonly string name;
        public readonly int maxHealth;
        public readonly double strength;

        public int Health { get; protected set; }

        public Pawn(string name, int maxHealth, double strength)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.strength = strength;
            Health = this.maxHealth;
        }

        public bool TakeDamage(int amount)
        {
            if (amount < 0) return false;

            Health -= amount;

            Console.WriteLine($"{name} получает {amount} ед урона.");

            return Health < 0;
        }

        public void Heal(int amount)
        {
            Health = Math.Min(maxHealth, Health + amount);
            Console.WriteLine($"{name} восстанавливает {amount} ед здоровья");
        }

        public override string ToString()
        {
            return $"{name} ({Health}/{maxHealth}, {strength})";
        }
    }
}
