using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.RemoveDuplicatesProblem
{
    public class RemoveDuplicates
    {
        public static int RemoveDuplicatesProblem(int[] nums)
        {
            if(nums.Length==0) return 0;

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
