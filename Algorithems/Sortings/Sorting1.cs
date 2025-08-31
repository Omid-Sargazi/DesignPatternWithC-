using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Sortings
{
    public class Sorting1
    {
        public class Sorting
        {
            public static void Bubble(int[] arr)
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
                    if (!swapped)
                        break;

                }
                Console.WriteLine(string.Join(",", arr)+" Bubble");

            }

            public static void Selection(int[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    int minIndex = i;
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[j] < arr[minIndex])
                            minIndex = j;
                    }

                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }

                Console.WriteLine(string.Join(",",arr)+" Selection");
            }


            public static void Insertion(int[] arr)
            {
                for (int i = 1; i < arr.Length; i++)
                {
                    int j= i-1;
                    int current = arr[i];
                    while (j>=0 && arr[j]>current)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }

                    arr[j+1] = current;
                }

                Console.WriteLine(string.Join(",",arr)+" Insertion ");
            }


            public static void MergeSorting(int[] arr)
            {
                if(arr.Length<=1) return;
                int n =arr.Length;
                int mid = n / 2;
                int[] left = new int[mid];
                int[] right = new int[n - mid];

                Array.Copy(arr,0,left,0,mid);
                Array.Copy(arr,mid,right,0,right.Length);

                Console.WriteLine(string.Join(",",left)+" left");
                Console.WriteLine(string.Join(",",right)+ " right");

                MergeSorting(left);
                MergeSorting(right);
                Merge(arr,left, right);



            }

            private static void Merge(int[] result, int[] left, int[] right)
            {
                int p1 = 0;
                int p2= 0;
                int p3 = 0;

                int n1 = left.Length-1;
                int n2 = right.Length-1;

                while (p1<=n1 && p2<=n2)
                {
                    if (left[p1] < right[p2])
                    {
                        result[p3] = left[p1];
                        p1++;
                    }
                    else
                    {
                        result[p3] = right[p2];
                        p2++;
                    }

                    p3++;
                }

                while (p1<= n1)
                {
                    result[p3] = left[p1];
                    p1++;
                    p3++;
                }

                while (p2<=n2)
                {
                    result[p3] = right[p2];
                    p2++;
                    p3++;
                }

                Console.WriteLine(string.Join(",",result)+" Merge Sorting;");
            }


            public static int PivotDemo(int[] arr, int lo, int hi)
            {
                int pivot = arr[hi];
                int i = lo - 1;
                for (int j = lo; j < hi; j++)
                {
                    if (arr[j] < pivot)
                    {

                        i++;
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
                }

                (arr[i + 1], arr[hi]) = (arr[hi], arr[i + 1]);
                Console.WriteLine(string.Join(",",arr)+$" Pivot : {pivot}");
                return i + 1;
            }
        }
    }
}
