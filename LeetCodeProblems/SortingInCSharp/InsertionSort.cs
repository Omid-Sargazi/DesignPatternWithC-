using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.SortingInCSharp
{
    public class InsertionSort
    {
        public static void Run(int[] arr)
        {
            for(int i=1;i<=arr.Length;i++)
            {
                int j = i - 1;
                int key = arr[i];
                while(j>=0 && arr[j] > key)
                {
                    j--;
                    arr[j+1]=arr[j];
                }
                arr[j+1] = key;
            }

            foreach(var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
