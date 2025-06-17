using System;
using System.Text;

public class Node
{
    public int Data { get; set; }
    public Node Next { get; set; } 
    public Node Previous { get; set; }

    public Node(int data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }
}
public class DoublyLinkedList
{
    public Node Head { get; private set; }
    public Node Tail { get; private set; }
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }
    public void Add(int data)
    {
        Node newNode = new Node(data);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
        }
        Count++;
    }

    public bool Remove(int data)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Data == data)
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    Head = current.Next;
                }
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    Tail = current.Previous;
                }

                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }
    public Node Search(int key)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Data == key)
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }
    public void Print()
    {
        if (Head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Node current = Head;
        StringBuilder sb = new StringBuilder();
        while (current != null)
        {
            sb.Append(current.Data);
            if (current.Next != null)
            {
                sb.Append(" <-> ");
            }
            current = current.Next;
        }
        Console.WriteLine(sb.ToString());
    }

    // --- РОЗВ'ЯЗАННЯ ЗАДАЧІ ЗГІДНО З ВАРІАНТОМ ---

    // 1. Видалити зі списку перший елемент, більший числа 4.
    public void RemoveFirstGreaterThan(int value)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Data > value)
            {
                Console.WriteLine($"\nЗнайдено перший елемент більший за {value}: {current.Data}. Видаляємо його...");
                Remove(current.Data);
                return;
            }
            current = current.Next;
        }
        Console.WriteLine($"\nЕлемент, більший за {value}, не знайдено.");
    }

    // 2. Вставити в список число 10 перед кожним числом, рівним 15.
    public void InsertBeforeValue(int valueToInsert, int targetValue)
    {
        Console.WriteLine($"\nВставляємо число {valueToInsert} перед кожним елементом зі значенням {targetValue}...");
        Node current = Head;
        while (current != null)
        {
            if (current.Data == targetValue)
            {
                Node newNode = new Node(valueToInsert);
                newNode.Next = current;
                newNode.Previous = current.Previous;
                if (current.Previous == null)
                {
                    Head = newNode;
                }
                else
                {
                    current.Previous.Next = newNode;
                }

                current.Previous = newNode;
                Count++;
            }
            current = current.Next;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DoublyLinkedList list = new DoublyLinkedList();

        Console.WriteLine("--- Початкове наповнення списку ---");
        list.Add(3);
        list.Add(15);
        list.Add(2);
        list.Add(7);
        list.Add(4);
        list.Add(15);
        list.Add(9);

        Console.Write("Початковий список: ");
        list.Print();
        Console.WriteLine("\n--- Демонстрація пошуку ---");
        int keyToSearch = 7;
        Node foundNode = list.Search(keyToSearch);
        if (foundNode != null)
        {
            Console.WriteLine($"Елемент з ключем {keyToSearch} знайдено.");
        }
        else
        {
            Console.WriteLine($"Елемент з ключем {keyToSearch} не знайдено.");
        }
        list.RemoveFirstGreaterThan(4);
        Console.Write("Список після видалення першого елемента > 4: ");
        list.Print();
        list.InsertBeforeValue(10, 15);
        Console.Write("Фінальний список: ");
        list.Print();
    }
}