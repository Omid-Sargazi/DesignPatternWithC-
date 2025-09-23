using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemsOfLeetCodes.Problem1
{
    public class Solution1
    {
        public int[] TwoSum(int[] nums, int target)
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
    }
}
