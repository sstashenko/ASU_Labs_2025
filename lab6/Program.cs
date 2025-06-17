using lab6;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введіть номер вашого варіанту (v): ");
        if (!int.TryParse(Console.ReadLine(), out int v) || v < 0)
        {
            Console.WriteLine("Некоректний номер варіанту. Встановлено v = 0.");
            v = 0;
        }

        int n = 25 + v;
        var random = new Random();
        var numbers = new List<int>();

        Console.WriteLine($"\nГенеруємо {n} випадкових чисел в діапазоні [-50, 50]...");
        for (int i = 0; i < n; i++)
        {
            numbers.Add(random.Next(-50, 51));
        }

        Console.WriteLine("Згенерована послідовність: " + string.Join(", ", numbers));

        var tree = new BinarySearchTree();
        foreach (var number in numbers)
        {
            tree.Add(number);
        }

        Console.WriteLine("\nБінарне дерево пошуку успішно створено.");
        
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("Оберіть операцію:");
            Console.WriteLine("1. Вивести структуру дерева");
            Console.WriteLine("2. Вивести відсортований вміст дерева");
            Console.WriteLine("3. Додати елемент");
            Console.WriteLine("4. Видалити елемент");
            Console.WriteLine("5. Знайти вузли, де |лівий син - правий син| <= 5");
            Console.WriteLine("6. Знайти вузли, де |сума правого піддерева - сума лівого| > 20");
            Console.WriteLine("7. Вийти");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    tree.PrintTree();
                    break;
                case "2":
                    tree.PrintSorted();
                    break;
                case "3":
                    Console.Write("Введіть значення для додавання: ");
                    if (int.TryParse(Console.ReadLine(), out int valToAdd))
                    {
                        tree.Add(valToAdd);
                        Console.WriteLine($"Елемент {valToAdd} додано.");
                        tree.PrintTree();
                    }
                    else
                    {
                        Console.WriteLine("Некоректне значення.");
                    }
                    break;
                case "4":
                    Console.Write("Введіть значення для видалення: ");
                     if (int.TryParse(Console.ReadLine(), out int valToDelete))
                    {
                        tree.Delete(valToDelete);
                        Console.WriteLine($"Елемент {valToDelete} видалено (якщо він існував).");
                        tree.PrintTree();
                    }
                    else
                    {
                        Console.WriteLine("Некоректне значення.");
                    }
                    break;
                case "5":
                    tree.FindNodesWithCloseChildren();
                    break;
                case "6":
                    tree.FindNodesWithLargeSubtreeSumDifference();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                    break;
            }
        }
    }
}