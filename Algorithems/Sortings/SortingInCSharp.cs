using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Sortings
{
    public class SortingInCSharp
    {
        public static void Bubble(int[] arr)
        {
            for (int start = arr.Length - 1; start >= 0; start--)
            {
                bool swapped = false;
                for (int j = 0; j < start; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if(!swapped) break;
            }

            Console.WriteLine($"{string.Join(", ",arr)}");
        }

        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] > arr[minIndex])
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    (arr[minIndex], arr[i]) = (arr[i], arr[minIndex]);
                }
            }

            Console.WriteLine($"{string.Join(", ",arr)}");
        }

        public static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int current = arr[i];
                int j = i - 1;
                while (j>=0&& arr[j]>current)
                {
                    arr[j+1] = arr[j];
                    j--;
                }
                arr[j+1] = current;
            }

            Console.WriteLine($"{string.Join(", ", arr)}"+" InsertionSort");

        }


        public static void QuickSort(int[] arr, int lo, int hi)
        {
            if (lo < hi)
            {
                int pi = Partition(arr, lo, hi);
                QuickSort(arr, lo, pi - 1);
                QuickSort(arr, pi + 1, hi);
            }
        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int i = lo - 1;
            for (int j = lo; j < hi; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[i + 1], arr[hi]) = (arr[hi], arr[i + 1]);

            return i + 1;
        }

        public static void RunQuickSort(int[] arr)
        {
            Console.WriteLine($"Before QuickSort: {string.Join(",", arr)}");
            QuickSort(arr,0,arr.Length-1);
            Console.WriteLine($"After QuickSort: {string.Join(",", arr)}");

        }


        public static void MergeSort(int[] arr)
        {
            if(arr.Length<=1) return;

            int n = arr.Length;
            int mid = n / 2;

            int[] left = new int[mid];
            int[] right = new int[n-mid];

            Array.Copy(arr,0,left,0,mid);
            Array.Copy(arr,mid,right,0,right.Length);

            MergeSort(left);
            MergeSort(right);
            Merge(arr,left,right);

            Console.WriteLine($"Merge Sort: {string.Join(",", arr)}");

        }

        private static void Merge(int[] result, int[] left, int[] right)
        {
            int p1 = 0;
            int p2 = 0;
            int p3 = 0;

            int n1 = left.Length;
            int n2 = right.Length;

            while (p1<n1 && p2<n2)
            {
                if (left[p1] < right[p2])
                {
                    result[p3] = left[p1];
                    p1++;
                }
                else
                {
                    result[p3] = right[p2];
                    p2++;
                }

                p3++;
            }

            while (p1<n1)
            {
                result[p3] = left[p1];
                p1++;
                p3++;
            }

            while (p2<n2)
            {
                result[p3] = right[p2];
                p2++;
                p3++;
            }
        }
    }

    
}
