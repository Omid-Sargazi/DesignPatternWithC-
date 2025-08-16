using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problem1
{
    public class InsertionSort
    {
        public static void Run(int[] arr)
        {
            for(int i=1;i<arr.Length;i++)
            {
                int key=arr[i];
                int j = i - 1;

                while(j>=0 && arr[j] > key)
                {
                    arr[j+1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
    }
}
