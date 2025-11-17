using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problems2
{
    public class LeetCodeProblems
    {
        public static int[] TwoSum(int[] arr, int target)
        {
            Dictionary<int,int> seen = new Dictionary<int,int>();

            for (int i = 0; i < arr.Length; i++)
            {
                int completed = target - arr[i];
                if (!seen.ContainsKey(completed))
                {
                    seen[completed] = i;
                }

                return new int[] { i, seen[completed] };
            }

            return new int[] { -1, -1 };
        }

        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> seen = new HashSet<int>();

            foreach (var num in nums)
            {
                if (seen.Contains(num))
                {
                    return true;
                }
                seen.Add(num);
            }
            return false;
        }

        public static bool ContainsDuplicateLinq(int[] nums)
        {
            return nums.Length !=nums.Distinct().Count();
        }


    }
}
