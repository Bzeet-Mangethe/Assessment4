using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<T> queue;
    private IComparer<T> comparer;

    public PriorityQueue(IComparer<T> comparer)
    {
        this.queue = new List<T>();
        this.comparer = comparer;
    }

    public PriorityQueue() : this(Comparer<T>.Default)
    {
    }

    public bool IsEmpty => queue.Count == 0;

    public int Count => queue.Count;

    public void Enqueue(T item)
    {
        queue.Add(item);
        int currentIndex = queue.Count - 1;
        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;
            if (comparer.Compare(queue[currentIndex], queue[parentIndex]) >= 0)
                break;

            Swap(currentIndex, parentIndex);
            currentIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (queue.Count == 0)
            throw new InvalidOperationException("PriorityQueue is empty");

        T removedItem = queue[0];
        int lastIndex = queue.Count - 1;
        queue[0] = queue[lastIndex];
        queue.RemoveAt(lastIndex);

        int currentIndex = 0;
        while (true)
        {
            int leftChildIndex = 2 * currentIndex + 1;
            int rightChildIndex = 2 * currentIndex + 2;
            int smallestChildIndex = currentIndex;

            if (leftChildIndex < queue.Count && comparer.Compare(queue[leftChildIndex], queue[smallestChildIndex]) < 0)
                smallestChildIndex = leftChildIndex;

            if (rightChildIndex < queue.Count && comparer.Compare(queue[rightChildIndex], queue[smallestChildIndex]) < 0)
                smallestChildIndex = rightChildIndex;

            if (smallestChildIndex == currentIndex)
                break;

            Swap(currentIndex, smallestChildIndex);
            currentIndex = smallestChildIndex;
        }

        return removedItem;
    }

    private void Swap(int index1, int index2)
    {
        T temp = queue[index1];
        queue[index1] = queue[index2];
        queue[index2] = temp;
    }
}

// Console application
public class Program
{
    public static void Main(string[] args)
    {
        // Create a priority queue of integers
        PriorityQueue<int> pqInt = new PriorityQueue<int>();

        // Enqueue elements
        pqInt.Enqueue(10);
        pqInt.Enqueue(30);
        pqInt.Enqueue(20);

        // Dequeue elements
        Console.WriteLine(pqInt.Dequeue());  // Output: 10
        Console.WriteLine(pqInt.Dequeue());  // Output: 20

        // Create a priority queue of strings
        PriorityQueue<string> pqStr = new PriorityQueue<string>();

        // Enqueue elements
        pqStr.Enqueue("Apple");
        pqStr.Enqueue("Banana");
        pqStr.Enqueue("Cherry");

        // Dequeue elements
        Console.WriteLine(pqStr.Dequeue());  // Output: Apple
        Console.WriteLine(pqStr.Dequeue());  // Output: Banana
    }
}

