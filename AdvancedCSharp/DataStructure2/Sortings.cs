using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DataStructure2
{
    public class Sortings
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

                if (!swapped)
                {
                    break;
                }
            }

            Console.WriteLine($"{string.Join(",",arr)}");
        }

        public static void Selection(int[] arr)
        {
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

                (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }

            Console.WriteLine($"{string.Join(",",arr)}");
        }

        public static void Insertion(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int currrnt = arr[i];
                int j = i - 1;
                while (j>=0 && arr[j]>currrnt)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j+1] = currrnt;
            }

            Console.WriteLine($"{string.Join(",", arr)}");

        }

        public static void QuickSort(int[] arr, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = Partition(arr, lo, hi);
                QuickSort(arr, lo, p-1);
                QuickSort(arr, p + 1, hi);
            }
            Console.WriteLine($"{string.Join(",", arr)}");

        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int j = lo-1;
            for (int i = lo; i < hi; i++)
            {
                if (arr[i] < pivot)
                {
                    j++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }

            (arr[j+1], arr[hi]) = (arr[hi], arr[j+1]);
            return j+1;
        }
    }
}
