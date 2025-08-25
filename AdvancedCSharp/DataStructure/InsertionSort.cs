using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DataStructure
{
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
                    arr[j+1] = arr[j];
                    j--;
                }

                arr[j + 1] = current;
            }

            Console.WriteLine(string.Join(",",arr));
        }
    }
}
