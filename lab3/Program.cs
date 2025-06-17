using System;
using System.Text;

public class Node
{
    public string Data { get; set; } 
    public Node Next { get; set; } 

    public Node(string data)
    {
        Data = data;
        Next = null;
    }
}
public class SinglyLinkedList
{
    private Node head;
    private Node tail;

    public SinglyLinkedList()
    {
        head = null;
        tail = null;
    }
    public void Add(string data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
        Console.WriteLine($"Додано '{data}' до списку.");
    }
    public bool Remove(string data)
    {
        if (head == null) return false;

        if (head.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            head = head.Next;
            if (head == null)
            {
                tail = null;
            }
            return true;
        }

        Node current = head;
        while (current.Next != null && !current.Next.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            if (current.Next == tail)
            {
                tail = current;
            }
            current.Next = current.Next.Next;
            return true;
        }

        return false;
    }

    public Node Find(string data)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(data, StringComparison.OrdinalIgnoreCase))
            {
                return current;
            }
            current = current.Next;
        }
        return null; 
    }

    public void Print(string title)
    {
        Console.WriteLine($"\n--- {title} ---");
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Node current = head;
        StringBuilder sb = new StringBuilder();
        while (current != null)
        {
            sb.Append(current.Data);
            if (current.Next != null)
            {
                sb.Append(" -> ");
            }
            current = current.Next;
        }
        Console.WriteLine(sb.ToString());
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        SinglyLinkedList mainSquad = new SinglyLinkedList();
        SinglyLinkedList substituteSquad = new SinglyLinkedList();

        Console.WriteLine("--- Формування основного складу ---");
        mainSquad.Add("Шевченко");
        mainSquad.Add("Ребров");
        mainSquad.Add("Шовковський");
        mainSquad.Add("Ярмоленко");
        mainSquad.Add("Зінченко");

        Console.WriteLine("\n--- Формування запасного складу ---");
        substituteSquad.Add("Миколенко");
        substituteSquad.Add("Циганков");
        substituteSquad.Add("Мудрик");

        mainSquad.Print("Початковий основний склад");
        substituteSquad.Print("Початковий запасний склад");

        PerformSubstitutions(mainSquad, substituteSquad);

        mainSquad.Print("Фінальний основний склад");
        substituteSquad.Print("Фінальний запасний склад");
    }

    static void PerformSubstitutions(SinglyLinkedList main, SinglyLinkedList subs)
    {
        Console.Write("\nВведіть кількість замін (K), яку потрібно провести: ");
        if (!int.TryParse(Console.ReadLine(), out int k) || k <= 0)
        {
            Console.WriteLine("Некоректне значення. Заміни не будуть проводитись.");
            return;
        }

        for (int i = 0; i < k; i++)
        {
            Console.WriteLine($"\n--- Проведення заміни {i + 1} з {k} ---");
            main.Print("Поточний основний склад");
            subs.Print("Поточний запасний склад");

            Console.Write("Введіть прізвище гравця з основного складу для заміни: ");
            string playerOut = Console.ReadLine();

            Console.Write("Введіть прізвище гравця з запасу для виходу на поле: ");
            string playerIn = Console.ReadLine();

            if (main.Find(playerOut) != null && subs.Find(playerIn) != null)
            {
                main.Remove(playerOut);
                subs.Remove(playerIn);

                main.Add(playerIn);
                subs.Add(playerOut);

                Console.WriteLine($"\nЗаміна успішна! {playerIn} вийшов на поле, {playerOut} сів на лаву запасних.");
            }
            else
            {
                Console.WriteLine("Помилка: одного з гравців не знайдено у відповідному списку. Спробуйте ще раз.");
                i--;
            }
        }
    }
}