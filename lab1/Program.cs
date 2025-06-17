using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class Program
{
    public struct PerformanceResult
    {
        public string DataSetDescription;
        public string Algorithm;
        public double TimeMs;
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var sortingResults = new List<PerformanceResult>();
        var searchResults = new List<PerformanceResult>();
        HashSet<int> lastIntersectionResult = null;
        
        Console.WriteLine("Генерація даних та проведення аналізу... Будь ласка, зачекайте.");

        var scenarios = new List<(string description, int[] arrayA, int[] arrayB)>();

        int[] sizes = { 10, 100, 1000, 10000 };
        foreach (var size in sizes)
        {
            var (a, b) = DataGenerator.GenerateRandomArrays(size);
            scenarios.Add(($"Випадкові ({size})", a, b));
        }

        var (largeA, largeB) = DataGenerator.GenerateRandomArrays(10000);
        var (almostSortedA, almostSortedB) = DataGenerator.CreateAlmostSortedArrays(largeA, largeB);
        var (reverseSortedA, reverseSortedB) = DataGenerator.CreateReverseSortedArrays(largeA, largeB);
        scenarios.Add(("Майже відсорт. (10k)", almostSortedA, almostSortedB));
        scenarios.Add(("Зворотньо сорт. (10k)", reverseSortedA, reverseSortedB));

        foreach (var scenario in scenarios)
        {
            RunMeasurements(scenario.arrayA, scenario.arrayB, scenario.description, sortingResults, searchResults, out lastIntersectionResult);
        }

        Console.Clear(); 
        Console.WriteLine("Невідсортовані масиви (приклад для розміру 10):");
        Console.Write("A: "); PrintArraySample(scenarios.First().arrayA);
        Console.Write("B: "); PrintArraySample(scenarios.First().arrayB);
        Console.WriteLine("--------------------------------------------------------------------------");
        
        PrintPerformanceTable("Sorting Performance", sortingResults);
        PrintPerformanceTable("Search Performance (for Intersection)", searchResults);

        Console.WriteLine("Unique Elements (результат для останнього набору даних)");
        Console.WriteLine("--------------------------------------------------------------------------");
        if (lastIntersectionResult == null || lastIntersectionResult.Count == 0)
        {
            Console.WriteLine("Спільних елементів не знайдено.");
        }
        else
        {
            Console.WriteLine($"Знайдено {lastIntersectionResult.Count} унікальних спільних елементів:");
            PrintArraySample(lastIntersectionResult.ToArray());
        }
        Console.WriteLine("--------------------------------------------------------------------------");
        
        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }

    public static void RunMeasurements(int[] a, int[] b, string description, 
                                     List<PerformanceResult> sortingResults, 
                                     List<PerformanceResult> searchResults, 
                                     out HashSet<int> intersection)
    {
        var stopwatch = new Stopwatch();

        int[] aForInsertion = (int[])a.Clone();
        stopwatch.Restart();
        Sorting.InsertionSort(aForInsertion);
        stopwatch.Stop();
        sortingResults.Add(new PerformanceResult { DataSetDescription = description, Algorithm = "Insertion Sort", TimeMs = stopwatch.Elapsed.TotalMilliseconds });

        int[] aForQuick = (int[])a.Clone();
        stopwatch.Restart();
        Sorting.QuickSort(aForQuick, 0, aForQuick.Length - 1);
        stopwatch.Stop();
        sortingResults.Add(new PerformanceResult { DataSetDescription = description, Algorithm = "Quick Sort", TimeMs = stopwatch.Elapsed.TotalMilliseconds });

        stopwatch.Restart();
        Search.FindIntersectionLinear(a, b);
        stopwatch.Stop();
        searchResults.Add(new PerformanceResult { DataSetDescription = description, Algorithm = "Linear Search (on unsorted)", TimeMs = stopwatch.Elapsed.TotalMilliseconds });
        
        int[] sortedB = (int[])b.Clone();
        Sorting.QuickSort(sortedB, 0, sortedB.Length - 1);
        stopwatch.Restart();
        intersection = Search.FindIntersectionBinary(a, sortedB);
        stopwatch.Stop();
        searchResults.Add(new PerformanceResult { DataSetDescription = description, Algorithm = "Binary Search (on sorted)", TimeMs = stopwatch.Elapsed.TotalMilliseconds });
    }

    public static void PrintPerformanceTable(string title, List<PerformanceResult> results)
    {
        Console.WriteLine($"\n{title}");
        Console.WriteLine("--------------------------------------------------------------------------");
        Console.WriteLine($"| {"Data Set",-25} | {"Algorithm",-28} | {"Time (ms)",-15} |");
        Console.WriteLine("--------------------------------------------------------------------------");
        foreach (var res in results)
        {
            Console.WriteLine($"| {res.DataSetDescription,-25} | {res.Algorithm,-28} | {res.TimeMs,-15:F4} |");
        }
        Console.WriteLine("--------------------------------------------------------------------------");
    }

    public static void PrintArraySample(int[] array)
    {
        Console.WriteLine($"[{string.Join(", ", array.Take(15))}{(array.Length > 15 ? ", ..." : "")}]");
    }
}

public static class Sorting
{
    public static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    public static void QuickSort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(array, low, high);
            QuickSort(array, low, pi - 1);
            QuickSort(array, pi + 1, high);
        }
    }

    private static int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }
}

public static class Search
{
    private static bool LinearSearch(int[] array, int target)
    {
        foreach (var item in array)
        {
            if (item == target) return true;
        }
        return false;
    }

    private static bool BinarySearch(int[] sortedArray, int target)
    {
        int left = 0;
        int right = sortedArray.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (sortedArray[mid] == target) return true;
            if (sortedArray[mid] < target) left = mid + 1;
            else right = mid - 1;
        }
        return false;
    }
    
    public static HashSet<int> FindIntersectionLinear(int[] a, int[] b)
    {
        var intersection = new HashSet<int>();
        var uniqueA = new HashSet<int>(a); 
        foreach (var itemA in uniqueA)
        {
            if (LinearSearch(b, itemA))
            {
                intersection.Add(itemA);
            }
        }
        return intersection;
    }

    public static HashSet<int> FindIntersectionBinary(int[] a, int[] sortedB)
    {
        var intersection = new HashSet<int>();
        var uniqueA = new HashSet<int>(a);
        foreach (var itemA in uniqueA)
        {
            if (BinarySearch(sortedB, itemA))
            {
                intersection.Add(itemA);
            }
        }
        return intersection;
    }
}

public static class DataGenerator
{
    private static readonly Random _random = new Random();

    public static (int[], int[]) GenerateRandomArrays(int size)
    {
        int[] a = new int[size];
        int[] b = new int[size];
        for (int i = 0; i < size; i++)
        {
            a[i] = _random.Next(0, size * 2);
            b[i] = _random.Next(0, size * 2);
        }
        return (a, b);
    }
    
    public static (int[], int[]) CreateAlmostSortedArrays(int[] a, int[] b)
    {
        int[] sortedA = (int[])a.Clone(); Array.Sort(sortedA);
        int[] sortedB = (int[])b.Clone(); Array.Sort(sortedB);
        int swaps = a.Length / 10;
        for (int i = 0; i < swaps; i++)
        {
            int index1 = _random.Next(a.Length); int index2 = _random.Next(a.Length);
            (sortedA[index1], sortedA[index2]) = (sortedA[index2], sortedA[index1]);
            index1 = _random.Next(b.Length); index2 = _random.Next(b.Length);
            (sortedB[index1], sortedB[index2]) = (sortedB[index2], sortedB[index1]);
        }
        return (sortedA, sortedB);
    }
    
    public static (int[], int[]) CreateReverseSortedArrays(int[] a, int[] b)
    {
        int[] reverseA = (int[])a.Clone(); Array.Sort(reverseA); Array.Reverse(reverseA);
        int[] reverseB = (int[])b.Clone(); Array.Sort(reverseB); Array.Reverse(reverseB);
        return (reverseA, reverseB);
    }
}