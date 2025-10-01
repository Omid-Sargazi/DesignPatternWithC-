using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndLeetCode.TwoSumProblems
{
    public class Problems
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> seen = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int completed = target - nums[i];
                if (seen.TryGetValue(completed, out int value))
                {
                    return new[] { i, value };
                }
                seen[nums[i]] = i;
            }

            return new[] { 0 };
        }

        public static int BestTimeBuySellStock(int[] arr)
        {
            int minPrice = arr[0];
            int maxProfit = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                int profit = 0;
                if (arr[i]<minPrice)
                    minPrice = arr[i];
                else
                {
                    profit = arr[i] - minPrice;
                }

                if (maxProfit < profit)
                {
                    maxProfit = profit;
                }
            }

            return maxProfit;
        }

        public static bool ContainsDuplicate(int[] nums)
        {
           Dictionary<int,int> seen = new Dictionary<int,int>();

           for (int i = 0; i < nums.Length; i++)
           {
               if (seen.TryGetValue(nums[i], out int val))
               {
                   return true;
               }

               seen[nums[i]] = i;
           }
           return false;
        }

        public static bool ContainsDuplicate2(int[] nums)
        {
            HashSet<int> seen = new HashSet<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (seen.Add(nums[i]))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
