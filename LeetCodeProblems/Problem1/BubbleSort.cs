using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problem1
{
    public class BubbleSort
    {
        public static void Run(int[] arr)
        {
            for(int start=arr.Length-1;start>=0;start--)
            {
                bool swapped=false;
                for(int j=0;j<=start-1;j++)
                {
                    if (arr[j] > arr[j+1])
                    {
                        (arr[j], arr[j+1])=(arr[j+1], arr[j]);
                        swapped=true;
                    }
                }
                    if (!swapped) break;
            }

            foreach(var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
