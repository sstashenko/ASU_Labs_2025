using System;
using System.IO;

public class ClosedHashTable
{
    private string[] table;
    private int size;

    public ClosedHashTable(int capacity)
    {
        size = GetNextPrime(capacity);
        table = new string[size];
    }

    private int HashFunction(string key)
    {
        if (key.Length != 10)
        {
            return 0; 
        }
        string variablePart = key.Substring(2, 5);
        int number = int.Parse(variablePart);
        return number % size;
    }

    public bool Insert(string key)
    {
        int initialHash = HashFunction(key);
        int i = 0;

        while (i < size)
        {
            int index = (initialHash + i) % size;
            if (table[index] == null)
            {
                table[index] = key;
                Console.WriteLine($"-> Елемент {key} вставлено в комірку {index}.");
                return true;
            }
            if (table[index] == key)
            {
                Console.WriteLine($"-> Елемент {key} вже існує в таблиці.");
                return true;
            }

            Console.WriteLine($"-> Колізія для ключа {key} в комірці {index}. Шукаємо далі...");
            i++;
        }

        Console.WriteLine($"-> Помилка: Хеш-таблиця повна. Неможливо вставити {key}.");
        return false;
    }

    public void PrintTableToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Стан хеш-таблиці (розмір: {size}):");
            writer.WriteLine("-----------------------------------");
            for (int i = 0; i < size; i++)
            {
                writer.WriteLine($"[{i,2}]: {table[i] ?? "порожньо"}");
            }
            writer.WriteLine("-----------------------------------");
        }
    }
    
    private int GetNextPrime(int number)
    {
        while (true)
        {
            if (IsPrime(number))
                return number;
            number++;
        }
    }

    private bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;
        for (int i = 5; i * i <= number; i = i + 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }
        return true;
    }
}