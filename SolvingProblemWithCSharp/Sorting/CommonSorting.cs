using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolvingProblemWithCSharp.Sorting
{
    public class CommonSorting
    {
        public static void BubbleSort(int[] arr)
        {
            Console.WriteLine($"Before Sorting:  {string.Join(",",arr)}");
            int n = arr.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                bool swapped = false;
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if(swapped==false) break;
            }
            Console.WriteLine($"After Sorting:  {string.Join(",",arr)}");
        }

        public static void SelectionSort(int[] arr)
        {
            Console.WriteLine($"Before Sorting:  {string.Join(",", arr)}");
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                   
                }
                if (arr[i] != arr[minIndex])
                {
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }
            }
            Console.WriteLine($"After Sorting:  {string.Join(",", arr)}");
        }

        public static void InsertionSort(int[] arr)
        {
            Console.WriteLine($"Before Insertion Sorting:  {string.Join(",", arr)}");
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                int current = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j]>current)
                {
                    
                }
            }
            Console.WriteLine($"Before Insertion Sorting:  {string.Join(",", arr)}");

        }
    }
}
