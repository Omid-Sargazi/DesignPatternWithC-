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
    }
}
