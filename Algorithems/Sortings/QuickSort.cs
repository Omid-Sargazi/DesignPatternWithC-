using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Sortings
{
    public class QuickSorting
    {
        public static void QuickSort(int[] arr, int lo, int hi)
        {
            int p = Partition(arr, lo, hi);
            QuickSort(arr, lo, p - 1);
            QuickSort(arr, p + 1, hi);
        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int j = lo - 1;
            for (int i = lo; i < hi; i++)
            {
                if (arr[i] < pivot)
                {
                    j++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[j + 1], arr[hi]) = (arr[hi], arr[j + 1]);
            return j+1;
        }
    }
}
