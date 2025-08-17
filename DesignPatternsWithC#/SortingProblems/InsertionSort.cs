using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.SortingProblems
{
    public class InsertionSort
    {
        public static void Run(int[] arr)
        {
            for(int i=1;i<=arr.Length;i++)
            {
                int j = i - 1;
                int key = arr[i];
                while(j>=0 && arr[j]>key)
                {
                    arr[j+1]= arr[j];
                    j--;
                }
                arr[j+1]=key;
            }
        }
    }


    public class BubbleSort
    {
        public static void Run(int[] arr)
        {
            for(int start = arr.Length-1; start >=0; start--)
            {
                bool swapped = false;
                for(int j=0;j<=arr.Length;j++)
                {
                    if (arr[j] > arr[j+1])
                    {
                        (arr[j], arr[j+1]) = (arr[j+1],arr[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }
}
