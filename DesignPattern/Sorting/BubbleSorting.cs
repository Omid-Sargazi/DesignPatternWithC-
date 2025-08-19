using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Sorting
{
    public class BubbleSorting
    {
        public static void Run(int[] arr)
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

            Console.WriteLine(string.Join(",",arr));
        }
    }

    public class InsertionSort
    {
        public static void Run(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int j = i - 1;
                int current = arr[i];
                while (j>=0 && arr[j]>current)
                {
                   
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j+1] = current;
            }


            Console.WriteLine(string.Join(",",arr));
        }
    }
}
