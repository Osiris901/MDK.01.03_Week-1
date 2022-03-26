using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var elements = 0;
            var needInput = true;
            var rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);

            Console.WriteLine("Укажите количество элементов в массиве:");
            while (needInput)
            {
                var input = Console.ReadLine();
                if (Int32.TryParse(input, out int num))
                {
                    elements = num;
                    needInput = false;
                }
                else
                {
                    Console.WriteLine("Ввод не распознан. Попробуйте ещё раз...");
                }
            }

            var array = new int[elements];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(100);
            }

            Console.WriteLine("Введенный массив:");
            array.Print();

            Console.WriteLine("Перемешиваем массив...");
            var shuffled = array;
            shuffled.Shuffle();
            Console.WriteLine("После перемешивания:");
            shuffled.Print();

            Console.ReadKey();
        }
    }

    internal static class ArrayExtensions
    {
        public static void Shuffle<T>(this T[] array, Random rnd = null)
        {
            if (rnd == null)
                rnd = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);

            int n = array.Length;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                (array[k], array[n]) = (array[n], array[k]);
            }

        }

        public static void Print<T>(this T[] array)
        {
            Console.Write("[ ");
            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                {
                    Console.Write(array[i]);
                }
                else
                {
                    Console.Write(array[i] + ", ");
                }
            }
            Console.WriteLine(" ]");
        }
    }
}
