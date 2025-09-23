using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemsOfLeetCodes.Problem1
{
    public class Solution1
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left<right)
            {
                int currentSum = nums[left] + nums[right];
                if (currentSum == target)
                {
                    return new int[] { left, right };
                }
                else if(currentSum<target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return new[] {-1,-1 };
        }

        public static int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0) return 0;

            int uniquePointer = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] != nums[i - 1])
                {
                    nums[uniquePointer] = nums[i];
                    uniquePointer++;
                }
            }

            return uniquePointer;
        }


    }
}
