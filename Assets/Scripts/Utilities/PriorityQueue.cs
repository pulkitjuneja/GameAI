 using System;
using System.Collections.Generic;
 
 public class PriorityQueue<T> where T : IComparable<T>
  {
    private List<T> data;

    public PriorityQueue()
    {
      this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
      data.Add(item);
      int currentIndex = data.Count - 1;
      while (currentIndex > 0)
      {
        int parentIndex = (currentIndex - 1) / 2;
        if (data[currentIndex].CompareTo(data[parentIndex]) >= 0) {
          break;
        }
        T tmp = data[currentIndex]; 
        data[currentIndex] = data[parentIndex]; 
        data[parentIndex] = tmp;
        currentIndex = parentIndex;
      }
    }

    public T Dequeue()
    {
      int lastIndex = data.Count - 1;
      T frontItem = data[0];
      data[0] = data[lastIndex];
      data.RemoveAt(lastIndex);

      --lastIndex;
      int parentIndex = 0;
      while (true)
      {
        int currentIndex = parentIndex * 2 + 1; 
        if (currentIndex > lastIndex) {
          break;
        }
        int rightChild = currentIndex + 1;
        if (rightChild <= lastIndex && data[rightChild].CompareTo(data[currentIndex]) < 0)
          currentIndex = rightChild;
        if (data[parentIndex].CompareTo(data[currentIndex]) <= 0) {
          break;
        }
        T tmp = data[parentIndex]; 
        data[parentIndex] = data[currentIndex]; 
        data[currentIndex] = tmp; // swap parent and child
        parentIndex = currentIndex;
      }
      return frontItem;
    }

    public T Peek()
    {
      T frontItem = data[0];
      return frontItem;
    }

    public int Count()
    {
      return data.Count;
    }

    public override string ToString()
    {
      string s = "";
      for (int i = 0; i < data.Count; ++i)
        s += data[i].ToString() + " ";
      s += "count = " + data.Count;
      return s;
    }

    // public bool IsConsistent()
    // {
    //   // is the heap property true for all data?
    //   if (data.Count == 0) return true;
    //   int li = data.Count - 1; // last index
    //   for (int pi = 0; pi < data.Count; ++pi) // each parent index
    //   {
    //     int lcurrentIndex = 2 * pi + 1; // left child index
    //     int rci = 2 * pi + 2; // right child index

    //     if (lci <= li && data[pi].CompareTo(data[lci]) > 0) return false; // if lc exists and it's greater than parent then bad.
    //     if (rci <= li && data[pi].CompareTo(data[rci]) > 0) return false; // check the right child too.
    //   }
    //   return true; // passed all checks
    // }
  }
