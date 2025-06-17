using System;
using System.Text;
public class Node
{
    public double Data { get; set; }
    public Node Next { get; set; }
    public Node Previous { get; set; }

    public Node(double data)
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

    public void Add(double data)
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
    public bool Remove(double data)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Data == data)
            {
                RemoveNode(current);
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    private void RemoveNode(Node nodeToRemove)
    {
        if (nodeToRemove == null) return;

        if (nodeToRemove.Previous != null)
        {
            nodeToRemove.Previous.Next = nodeToRemove.Next;
        }
        else
        {
            Head = nodeToRemove.Next;
        }

        if (nodeToRemove.Next != null)
        {
            nodeToRemove.Next.Previous = nodeToRemove.Previous;
        }
        else
        {
            Tail = nodeToRemove.Previous;
        }

        Count--;
    }

    public Node Search(double key)
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

    // 1. Видалити зі списку елемент перед кожним елементом із значенням -2.
    public void RemoveBeforeValue(double targetValue)
    {
        Console.WriteLine($"\nВидаляємо елементи перед кожним значенням {targetValue}...");

        Node current = Head;
        while (current != null)
        {
            if (current.Data == targetValue && current.Previous != null)
            {
                Node nodeToRemove = current.Previous;
                Node nextNode = current.Next;
                Console.WriteLine($"Знайдено елемент {targetValue}, видаляємо попередній: {nodeToRemove.Data}");
                RemoveNode(nodeToRemove);

                current = current.Next;
            }
            else
            {
                current = current.Next;
            }
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
        list.Add(5.1);
        list.Add(8.7);
        list.Add(-2.0);
        list.Add(10.3);
        list.Add(4.5);
        list.Add(-2.0);
        list.Add(11.0);
        list.Add(-2.0);

        Console.Write("Початковий список: ");
        list.Print();

        list.RemoveBeforeValue(-2.0);
        Console.Write("Список після видалення елементів: ");
        list.Print();

        Console.WriteLine("\nВставляємо число 33 в кінець списку...");
        list.Add(33.0);
        Console.Write("Фінальний список: ");
        list.Print();
        
        Console.WriteLine("\n--- Додатковий тест ---");
        DoublyLinkedList testList = new DoublyLinkedList();
        testList.Add(-2.0);
        testList.Add(15.5);
        Console.Write("Тестовий список: ");
        testList.Print();
        testList.RemoveBeforeValue(-2.0);
        Console.Write("Тестовий список після операції: ");
        testList.Print();
    }
}