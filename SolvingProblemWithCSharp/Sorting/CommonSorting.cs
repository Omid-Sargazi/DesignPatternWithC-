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
    }
}
