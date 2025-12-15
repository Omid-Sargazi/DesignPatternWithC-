using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureInCSharp.Heap
{
    public class MaxHeap
    {
        private int[] heap;
        private int size;

        public MaxHeap(int[] array)
        {
            heap = array;
            size = array.Length;
        }


        private void Heapify(int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < size && heap[left] > heap[largest])
            {
                largest = left;
            }

            if (right < size && heap[right] > heap[largest])
            {
                largest = right;
            }

            if (largest != i)
            {
                (heap[i], heap[largest]) = (heap[largest], heap[i]);

                Heapify(largest);
            }
        }

        private void BuildMaxHeap()
        {
            for (int i = size / 2 - 1; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public void Print()
        {
            Console.WriteLine("Max-Heap array:");

            foreach (int val in heap)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();

            // نمایش درختی
            Console.WriteLine("Tree representation:");
            PrintTree(0, "");
        }

        private void PrintTree(int index, string indent)
        {
            if (index >= size) return;

            // چاپ فرزند راست
            PrintTree(2 * index + 2, indent + "    ");

            // چاپ گره فعلی
            Console.WriteLine(indent + heap[index]);

            // چاپ فرزند چپ
            PrintTree(2 * index + 1, indent + "    ");
        }
    }
}
