using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.DataStructure
{
    public class SumDivideConquer
    {
        public static int Run(int[] arr,int start, int end)
        {
            if(start == end) return arr[start];
            if(end - start==1) return arr[start] + arr[end];

            int mid = (start + end) / 2;
            int leftSum = Run(arr, start, mid);
            int rightSum = Run(arr, mid + 1, end);

            return leftSum + rightSum;
        }

        public static int FindMaxDivideConquer(int[] arr, int start, int end)
        {
            if (start == end) return arr[start];
            if (end - start == 1) return Math.Max(arr[start], arr[end]);

            int mid = (start + end) / 2;
            int leftMax = FindMaxDivideConquer(arr, start, mid);
            int rightMax = FindMaxDivideConquer(arr, mid + 1, end);

            return Math.Max(leftMax, rightMax);
        }

        public static double PowerDivideConquer(double x, int n)
        {
            if (n == 0) return 1;
            if(n == 1) return x;

            if (n < 0) return 1 / PowerDivideConquer(x, -n);


            double half = PowerDivideConquer(x, n / 2);

            if(n%2==0)
                return half * half;
            else
            {
                return half * half * x;
            }
        }
    }
}
