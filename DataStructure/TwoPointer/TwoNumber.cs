using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.TwoPointer
{
    public class TwoSunProblem
    {
        public static int[] TwoSum(int[] numbers, int target)
        {
            int left = 0;
            int right = numbers.Length - 1;

            while (left<right)
            {
                int currentSum = numbers[left] + numbers[right];

                if (currentSum == target)
                {
                    return new[] { left + 1, right + 1 };
                }
                else if(currentSum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return new int[] { -1, -1 };
        }
    }
}
