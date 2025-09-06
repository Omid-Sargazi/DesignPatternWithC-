using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Sortings
{
    public class Sorting3
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine($"Befor Heap: {string.Join(",",arr)}");
            //Heapify(arr,0,arr.Length);
            MaxHeap(arr);
            //MaxHeap(arr);
            SortHeap(arr);
            Console.WriteLine($"After Heap: {string.Join(",", arr)}");

        }

        private static void MaxHeap(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr,i,n);
            }
        }

        private static void SortHeap(int[] arr)
        {
            int n = arr.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]);
                Heapify(arr,0,i);
            }
        }

        private static void Heapify(int[] arr, int i,int n)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int largest = i;

            if (left < n && arr[left] > arr[largest])
            {
                largest = left;
            }

            if(right < n && arr[right] > arr[largest])
                largest = right;
            if (largest != i)
            {
                (arr[i], arr[largest]) = (arr[largest], arr[i]);
                Heapify(arr,largest,n);
            }
        }

        public static void BubbleSort(int[] arr)
        {
            for (int start = arr.Length - 1; start >= 0; start--)
            {
                bool swapped = false;
                for (int j = 0; j < arr.Length - 1; j++)
                {
                    if (arr[j] < arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if(!swapped) break;
            }

            Console.WriteLine($"Bubble Sorting: {string.Join(",",arr)}");
        }

        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }
            }
            Console.WriteLine($"Selection Sorting: {string.Join(",", arr)}");

        }

        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int current = arr[i];
                int j = i - 1;
                while (j>=0 && arr[j]>current)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = current;
            }

            Console.WriteLine($"Insertion Sorting: {string.Join(",", arr)}");

        }
    }
}
