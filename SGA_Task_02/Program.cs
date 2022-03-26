using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            
            while (input.ToLower() != "exit")
            {
                Console.WriteLine("\nВыберите действие:\n\thello - Приветствие\n\texit - выход\n");
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "hello":
                        Console.WriteLine("Привет!");
                        break;
                    case "exit":
                        Console.WriteLine("Выходим из программы...");
                        break;
                    default:
                        Console.WriteLine("Неизвестное действие.");
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
