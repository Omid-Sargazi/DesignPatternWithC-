using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Sorting
{
    public class DivideAndConqure
    {
        public static int Run(int[] arr,int start, int end)
        {
            if(start == end) return arr[start];

            int mid = (end + start) / 2;

            int maxleft = Run(arr, start, mid);
            int maxRight = Run(arr, mid + 1, end);

            return Math.Max(maxleft, maxRight);
        }

        public static int SumArray(int[] arr, int start, int end)
        {
            if(start == end) return arr[start];

            int mid = start+ ( end - start) / 2;

            int leftSum = SumArray(arr, start, mid);
            int rightSum = SumArray(arr, mid + 1, end);

            return leftSum + rightSum;
        }

        public static int BinarySearch(int[] arr, int start, int end, int target)
        {
            if (start > end) return -1;

            int mid = start + (end - start) / 2;

            if (arr[mid] == target) return mid;

            else if (arr[mid] > target)
                return BinarySearch(arr, start, mid - 1, target);
            else
            {
                return BinarySearch(arr, mid + 1, end, target);
            }
        }
    }
}
