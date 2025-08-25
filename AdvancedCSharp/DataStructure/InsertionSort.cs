using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DataStructure
{
    public class SelectionSort
    {
    public static void Run(int[] arr)
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


        Console.WriteLine(string.Join(",",arr));
    }
    }
}
