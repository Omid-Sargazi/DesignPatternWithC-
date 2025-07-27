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
            if(nums == null || nums.Length==0)
            {
                throw new ArgumentNullException("Array must not be empty.");
            }

            int maxVal = nums[0];
            foreach (int num in nums) 
            { 
                if(num >maxVal)
                    maxVal = num;
            }

            return maxVal;
    }
}
