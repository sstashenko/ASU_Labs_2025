using System;
using System.Collections.Generic;
using System.Linq; 

namespace QueueApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            var numberQueue = new Queue<int>();

            numberQueue.Enqueue(15);
            numberQueue.Enqueue(-8);
            numberQueue.Enqueue(0);
            numberQueue.Enqueue(101);
            numberQueue.Enqueue(-22);
            numberQueue.Enqueue(42);

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("--- Меню Роботи з Чергою ---");
                Console.WriteLine("1. Додати елемент (число) у чергу");
                Console.WriteLine("2. Видалити елемент з черги");
                Console.WriteLine("3. Переглянути наступний елемент у черзі (допустимий для видалення)");
                Console.WriteLine("4. Відобразити вміст черги");
                Console.WriteLine("5. Порахувати кількість додатних елементів");
                Console.WriteLine("6. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNumber(numberQueue);
                        break;
                    case "2":
                        RemoveNumber(numberQueue);
                        break;
                    case "3":
                        PeekNumber(numberQueue);
                        break;
                    case "4":
                        DisplayQueue(numberQueue);
                        break;
                    case "5":
                        CountPositiveNumbers(numberQueue);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\nНевірний вибір. Спробуйте ще раз.");
                        break;
                }
                if (running)
                {
                    Pause();
                }
            }
        }
        static void AddNumber(Queue<int> queue)
        {
            Console.WriteLine("\n--- Додавання нового числа ---");
            int number;
            while (true)
            {
                Console.Write("Введіть ціле число для додавання в чергу: ");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    break;
                }
                Console.WriteLine("Некоректне введення. Будь ласка, введіть ціле число.");
            }

            queue.Enqueue(number);
            Console.WriteLine($"\nЧисло {number} успішно додано в кінець черги.");
        }
        static void RemoveNumber(Queue<int> queue)
        {
            Console.WriteLine("\n--- Видалення елемента з черги ---");
            if (queue.Count > 0)
            {
                int removedNumber = queue.Dequeue();
                Console.WriteLine($"Видалено елемент з початку черги: {removedNumber}");
            }
            else
            {
                Console.WriteLine("Черга порожня. Немає чого видаляти.");
            }
        }

        static void PeekNumber(Queue<int> queue)
        {
            Console.WriteLine("\n--- Перегляд наступного елемента ---");
            if (queue.Count > 0)
            {
                int nextNumber = queue.Peek();
                Console.WriteLine($"Наступний елемент у черзі (доступний для видалення): {nextNumber}");
            }
            else
            {
                Console.WriteLine("Черга порожня.");
            }
        }
        static void DisplayQueue(Queue<int> queue)
        {
            Console.WriteLine("\n--- Вміст черги ---");
            if (queue.Count == 0)
            {
                Console.WriteLine("Черга порожня.");
                return;
            }
            Console.WriteLine("Елементи від початку до кінця:");
            Console.WriteLine($"[ {string.Join(", ", queue)} ]");
        }
        static void CountPositiveNumbers(Queue<int> queue)
        {
            Console.WriteLine("\n--- Підрахунок кількості додатних елементів ---");
            if (queue.Count == 0)
            {
                Console.WriteLine("Черга порожня. Кількість додатних елементів: 0.");
                return;
            }
            int positiveCount = queue.Count(number => number > 0);

            Console.WriteLine($"Кількість додатних елементів у черзі: {positiveCount}");
        }
        static void Pause()
        {
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }
    }
}