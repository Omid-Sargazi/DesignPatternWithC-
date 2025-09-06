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
            Heapify(arr,0,arr.Length);
            Console.WriteLine($"After Heap: {string.Join(",", arr)}");

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
    }
}
