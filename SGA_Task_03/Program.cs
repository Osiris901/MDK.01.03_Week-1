using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_03
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var secret = "Вы правильно ввели пароль!";
            var key = "1234";

            var input = "";
            var attempts = 0;

            Console.WriteLine("Для просмотра секретного сообщения введите пароль:");

            while (input != key && attempts < 3)
            {
                input = Console.ReadLine();

                if (input == key)
                {
                    Console.WriteLine(secret);
                }
                else
                {
                    Console.WriteLine($"Неправильно введен пароль! Осталось попыток {2 - attempts}...");
                }

                attempts++;
            }

            Console.ReadKey();
        }
    }
}
