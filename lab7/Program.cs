using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Лабораторна робота: Хеш-таблиця (закрите хешування)");

        Console.Write("Введіть бажану кількість елементів для таблиці: ");
        int capacity = int.Parse(Console.ReadLine());
        
        var hashTable = new ClosedHashTable(capacity);
        
        Console.WriteLine("Оберіть режим:");
        Console.WriteLine("1. Найкращий випадок (мінімум колізій)");
        Console.WriteLine("2. Найгірший випадок (максимум колізій)");
        Console.WriteLine("3. Ручне введення");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                GenerateBestCase(hashTable, capacity);
                break;
            case "2":
                GenerateWorstCase(hashTable, capacity);
                break;
            case "3":
                ManualInput(hashTable);
                break;
            default:
                Console.WriteLine("Невірний вибір. Завершення програми.");
                return;
        }

        string outputFilePath = "hash_table_report.txt";
        hashTable.PrintTableToFile(outputFilePath);
        Console.WriteLine($"\nЗвіт про стан хеш-таблиці збережено у файл: {outputFilePath}");
    }

    static void ManualInput(ClosedHashTable hashTable)
    {
        Console.WriteLine("\nВводьте ІПН (10 цифр, формат 77xxxxx54x). Для завершення введіть 'exit'.");
        while (true)
        {
            Console.Write("Введіть ІПН: ");
            string input = Console.ReadLine();
            if (input.ToLower() == "exit")
            {
                break;
            }

            if (input.Length == 10 && input.StartsWith("77") && input.EndsWith("54") && long.TryParse(input, out _))
            {
                hashTable.Insert(input);
            }
            else
            {
                Console.WriteLine("Невірний формат ІПН. Спробуйте ще раз.");
            }
        }
    }

    static void GenerateBestCase(ClosedHashTable hashTable, int count)
    {
        Console.WriteLine("\n--- Демонстрація найкращого випадку ---");
        for (int i = 0; i < count; i++)
        {
            string middlePart = i.ToString("D5");
            string ipn = $"77{middlePart}540";
            hashTable.Insert(ipn);
        }
    }
    static void GenerateWorstCase(ClosedHashTable hashTable, int count)
    {
        Console.WriteLine("\n--- Демонстрація найгіршого випадку ---");
        int tableSize = new ClosedHashTable(count).GetType().GetField("size", System.Reflection.BindingFlags.NonPublic |
                            System.Reflection.BindingFlags.Instance).GetValue(new ClosedHashTable(count)).GetHashCode();
        for (int i = 0; i < count; i++)
        {
            string middlePart = (i * tableSize).ToString("D5");
            string ipn = $"77{middlePart}540";
            hashTable.Insert(ipn);
        }
    }
}