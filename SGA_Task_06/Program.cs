using System;
using System.Collections.Generic;
using System.Linq;

namespace SGA_Task_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var isActive = true;

            while (isActive)
            {
                int option = MainMenu();
                switch (option)
                {
                    case 1:
                        Show();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Remove();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                        isActive = false;
                        break;
                    default:
                        Console.WriteLine("Ввод не распознан. Нажмите любую клавишу для возвращения в меню...");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Программа завершена. Нажмите любую клавишу чтобы закрыть это окно...");
            Console.ReadKey();
        }

        static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Программа учета работников.\n\nВыберите действие:\n\t[1] Список досье\n\t[2] Добавить досье\n\t[3] Удалить досье\n\t[4] Поиск по фамилии\n\t[5] Выход");

            return GetNumericInput(5);
        }

        static void Show()
        {
            int c = 0;

            Console.Clear();
            Console.WriteLine("Список работников:");
            foreach (var employee in Person.Employees)
            {
                Console.WriteLine($"{++c}) {employee}");
            }

            if (c == 0)
            {
                Console.WriteLine("\tСписок работников пуст!");
            }

            Console.WriteLine("Нажмите любую клавишу для возвращения в меню...");
            Console.ReadKey();
        }

        static void Add()
        {
            Console.Clear();

            Console.WriteLine("\nВведите ФИО работника:");
            var fio = Console.ReadLine();


            Console.WriteLine("\nВведите должность работника:");
            var position = Console.ReadLine();

            var p = new Person(fio, position);
            Console.WriteLine($"Добавить работника {p} в базу?\n\t[1] Да\n\t[2] Нет");
            int n = GetNumericInput(2);
            if (n == 1)
            {
                Console.WriteLine("Работник успешно добавлен.");
                Person.Employees.Add(p);
            } 

            Console.WriteLine("Нажмите любую клавишу для возвращения в меню...");
            Console.ReadKey();
        }

        static void Remove()
        {
            Console.Clear();
            Show();

            Console.WriteLine("\nУкажите номер работника для его удаления:");
            int n = GetNumericInput(Person.Employees.Count);

            var person = Person.Employees.ElementAt(n - 1);
            Person.Employees.Remove(person);
            Console.WriteLine($"Удаляем работника {person} из базы.\nНажмите любую клавишу чтобы вернуться в меню...");
            Console.ReadKey();
        }

        static void Search()
        {
            Console.Clear();
            Console.WriteLine("Введите фамилию для поиска:");
            var secondName = Console.ReadLine();

            var results = Person.Employees.Where(p => p.Name.Split().First().ToLower() == secondName.ToLower());

            Console.Clear();
            Console.WriteLine($"По вашему запросу \"{secondName}\" было найдено {results.Count()} результатов:");

            if (results.Count() == 0)
            {
                Console.WriteLine("Результатов не найдено.");
            }
            else
            {
                int c = 0;
                foreach (var result in results)
                {
                    Console.WriteLine($"{++c}) {result}");
                }
            }

            Console.WriteLine("\n\n Нажмите любую клавишу чтобы вернуться в меню...");
            Console.ReadKey();
        }

        static int GetNumericInput(int options, int start = 1)
        {
            var input = "";
            var needInput = true;

            while (needInput)
            {
                input = Console.ReadLine();
                if (Int32.TryParse(input, out int n))
                {
                    if (n >= start && n <= options)
                    {
                        needInput = false;
                        return n;
                    }
                }
                Console.WriteLine("Неверная комманда. Попробуйте ещё раз...");
            }

            return start;
        }
    }

    internal class Person
    {
        public static List<Person> Employees = new List<Person>
        {
            new Person("Кораблёв Максим Сергеевич", "авиаконструктор")
        };

        public readonly string Name;
        public readonly string Position;

        public Person(string name, string position)
        {
            Name = name;
            Position = position;
        }

        public override string ToString()
        {
            return $"{Name} - {Position}";
        }
    }
}
