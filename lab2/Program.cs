using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DynamicArray<T> : IEnumerable<T>
{
    private T[] _items;
    private int _count;

    public int Count => _count;
    public int Capacity => _items.Length;

    public DynamicArray(int capacity = 4)
    {
        _items = new T[capacity];
        _count = 0;
    }

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }

    public void Add(T item) => Insert(_count, item);

    public void Insert(int index, T item)
    {
        if (index < 0 || index > _count) throw new IndexOutOfRangeException();
        if (_count == _items.Length) Resize();
        for (int i = _count; i > index; i--) _items[i] = _items[i - 1];
        _items[index] = item;
        _count++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
        for (int i = index; i < _count - 1; i++) _items[i] = _items[i + 1];
        _count--;
        _items[_count] = default(T);
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_items[i], item)) return i;
        }
        return -1;
    }

    private void Resize()
    {
        int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
        T[] newItems = new T[newCapacity];
        Array.Copy(_items, newItems, _count);
        _items = newItems;
    }

    public override string ToString() => $"[{string.Join(", ", new ArraySegment<T>(_items, 0, _count))}]";
    public IEnumerator<T> GetEnumerator() { for (int i = 0; i < _count; i++) yield return _items[i]; }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var mainArray = new DynamicArray<double>();
        mainArray.Add(5.5);
        mainArray.Add(-12.1);
        mainArray.Add(0);
        mainArray.Add(4.2);
        mainArray.Add(1.0);
        mainArray.Add(-3.0);
        mainArray.Add(8.9);
        mainArray.Add(1);
        mainArray.Add(0);

        Console.WriteLine($"Початковий масив: {mainArray}\n");

        Console.WriteLine($"Максимальний за модулем елемент: {FindMaxAbsoluteValue(mainArray)}");
        Console.WriteLine($"Сума між першим і другим додатними: {SumBetweenFirstTwoPositives(mainArray)}\n");

        TransformArrayMoveZerosAndOnesToEnd(mainArray);
        Console.WriteLine($"Масив після перетворення (0 та 1 вкінці): {mainArray}");
    }

    // Завдання 1.1: Максимальний за модулем елемент
    public static double FindMaxAbsoluteValue(DynamicArray<double> array)
    {
        if (array.Count == 0) throw new InvalidOperationException("Масив порожній.");
        double maxAbsElement = array[0];
        for (int i = 1; i < array.Count; i++)
        {
            if (Math.Abs(array[i]) > Math.Abs(maxAbsElement))
            {
                maxAbsElement = array[i];
            }
        }
        return maxAbsElement;
    }

    // Завдання 1.2: Сума між першим і другим додатними
    public static double SumBetweenFirstTwoPositives(DynamicArray<double> array)
    {
        int firstPos = -1;
        for (int i = 0; i < array.Count; i++)
        {
            if (array[i] > 0)
            {
                if (firstPos == -1) firstPos = i;
                else
                {
                    double sum = 0;
                    for (int j = firstPos + 1; j < i; j++) sum += array[j];
                    return sum;
                }
            }
        }
        return 0;
    }

    // Завдання 2: Перетворити масив (нулі та одиниці в кінець) - ОПТИМІЗОВАНА ВЕРСІЯ O(N)
    public static void TransformArrayMoveZerosAndOnesToEnd(DynamicArray<double> array)
    {
        if (array.Count < 2) return;

        int writeIndex = 0;
        int zeroCount = 0;
        int oneCount = 0;

        for (int readIndex = 0; readIndex < array.Count; readIndex++)
        {
            if (array[readIndex] == 0) zeroCount++;
            else if (array[readIndex] == 1) oneCount++;
            else
            {
                array[writeIndex] = array[readIndex];
                writeIndex++;
            }
        }

        for (int i = 0; i < zeroCount; i++) array[writeIndex + i] = 0;
        for (int i = 0; i < oneCount; i++) array[writeIndex + zeroCount + i] = 1;
    }
}