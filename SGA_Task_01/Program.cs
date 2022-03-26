using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int gold = 0;
            int crystals = 0;

            int amount = 0;
            try
            {
                Console.WriteLine("Добро пожаловать в магазин!\nВведите начальное количество золота:");
                gold = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Сколько кристаллов вы хотите купить? (1 кристал = 100 золота)");
                amount = Int32.Parse(Console.ReadLine());

                var allowTransaction = Convert.ToInt32(gold >= amount * 100);
                gold -= amount * 100 * allowTransaction;
                crystals += amount * allowTransaction;

                Console.WriteLine($"Сделка совершена.\nТеперь у вас: {gold} золота, {crystals} кристаллов.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка ввода.\nНажмите любую клавишу для завершения программы.");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
