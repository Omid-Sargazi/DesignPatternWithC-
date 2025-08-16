using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problem1
{
    public class SelectionSort
    {
        public static void Run(int[] arr)
        {
            for(int i=0; i<=arr.Length-1;i++)
            {
                int minIndex = i;
                for(int j=i+1;j<=arr.Length-1;j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }

            }

            foreach(var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
