using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.LeetCodeProblems
{
    public class ArrayTraversal
    {
        public int FindMaximum(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                throw new ArgumentNullException("Array must not be empty.");
            }

            int maxVal = nums[0];
            foreach (int num in nums)
            {
                if (num > maxVal)
                    maxVal = num;
            }

            return maxVal;
        }


        public int[] TopKLargest_Sort(int[] nums, int k)
        {
            return nums.OrderByDescending(x => x).Take(k).ToArray();
        }


        public int[] TopKLargest_MinHeap(int[] nums, int k)
        {
            var minHeap = new PriorityQueue<int, int>();
            foreach (int num in nums)
            {
                minHeap.Enqueue(num, num);
                if (minHeap.Count > k)
                {
                    minHeap.Dequeue();
                }
            }

            return minHeap.UnorderedItems.Select(x => x.Element).ToArray();
        }
    }


}
