using System;
using System.Collections.Generic;
using System.Linq;

namespace lab5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            var diskStack = new Stack<Disk>();

            diskStack.Push(new Disk("System C:", 128.5));
            diskStack.Push(new Disk("Data D:", 931.0));
            diskStack.Push(new Disk("Backup E:", 465.5));

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("--- Головне Меню ---");
                Console.WriteLine("1. Робота зі стеком дисків");
                Console.WriteLine("2. Робота з чергою (не реалізовано)");
                Console.WriteLine("3. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageStack(diskStack);
                        break;
                    case "2":
                        Console.WriteLine("\nРобота з чергою не є частиною цього варіанту.");
                        Pause();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }
        static void ManageStack(Stack<Disk> stack)
        {
            bool inStackMenu = true;
            while (inStackMenu)
            {
                Console.Clear();
                Console.WriteLine("--- Меню Роботи зі Стеком ---");
                Console.WriteLine("1. Додати елемент (диск) у стек");
                Console.WriteLine("2. Видалити елемент зі стеку");
                Console.WriteLine("3. Переглянути верхній елемент стеку (допустимий для видалення)");
                Console.WriteLine("4. Відобразити вміст стеку");
                Console.WriteLine("5. Знайти диск з максимальним обсягом (Варіант 9)");
                Console.WriteLine("6. Повернутися до головного меню");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDisk(stack);
                        break;
                    case "2":
                        RemoveDisk(stack);
                        break;
                    case "3":
                        PeekDisk(stack);
                        break;
                    case "4":
                        DisplayStack(stack);
                        break;
                    case "5":
                        FindMaxVolumeDisk(stack);
                        break;
                    case "6":
                        inStackMenu = false;
                        break;
                    default:
                        Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                        break;
                }
                if (inStackMenu)
                {
                    Pause();
                }
            }
        }

        static void AddDisk(Stack<Disk> stack)
        {
            Console.WriteLine("\n--- Додавання нового диска ---");
            Console.Write("Введіть назву диска: ");
            string name = Console.ReadLine();

            double volume;
            while (true)
            {
                Console.Write("Введіть обсяг диска (ГБ): ");
                if (double.TryParse(Console.ReadLine(), out volume) && volume > 0)
                {
                    break;
                }
                Console.WriteLine("Некоректне значення. Будь ласка, введіть додатне число.");
            }

            var newDisk = new Disk(name, volume);
            stack.Push(newDisk);
            Console.WriteLine($"\nДиск \"{name}\" успішно додано до стеку.");
        }

        static void RemoveDisk(Stack<Disk> stack)
        {
            Console.WriteLine("\n--- Видалення елемента зі стеку ---");
            if (stack.Count > 0)
            {
                Disk removedDisk = stack.Pop();
                Console.WriteLine("Видалено елемент:");
                Console.WriteLine(removedDisk);
            }
            else
            {
                Console.WriteLine("Стек порожній. Немає чого видаляти.");
            }
        }

        static void PeekDisk(Stack<Disk> stack)
        {
            Console.WriteLine("\n--- Перегляд верхнього елемента ---");
            if (stack.Count > 0)
            {
                Disk topDisk = stack.Peek();
                Console.WriteLine("Верхній елемент у стеку (доступний для видалення):");
                Console.WriteLine(topDisk);
            }
            else
            {
                Console.WriteLine("Стек порожній.");
            }
        }

        static void DisplayStack(Stack<Disk> stack)
        {
            Console.WriteLine("\n--- Вміст стеку ---");
            if (stack.Count == 0)
            {
                Console.WriteLine("Стек порожній.");
                return;
            }

            Console.WriteLine("Елементи від верху до низу:");
            int i = 1;
            foreach (var disk in stack)
            {
                Console.WriteLine($"{i++}. {disk}");
            }
        }

        static void FindMaxVolumeDisk(Stack<Disk> stack)
        {
            Console.WriteLine("\n--- Пошук диска з максимальним обсягом ---");
            if (stack.Count == 0)
            {
                Console.WriteLine("Стек порожній. Пошук неможливий.");
                return;
            }

            Disk maxDisk = stack.MaxBy(d => d.VolumeGB);

            Console.WriteLine("Знайдено диск з максимальним обсягом:");
            Console.WriteLine(maxDisk);
        }
        static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }
    }
}