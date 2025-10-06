using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureInCSharp.Sorting
{
    public class KindOfSorting
    {
        public static void Bubble(int[] nums)
        {
            for (int start = nums.Length - 1; start >= 0; start--)
            {
                bool swapped = false;
                for (int j = 0; j < start; j++)
                {
                    if (nums[j] > nums[j + 1])
                    {
                        (nums[j], nums[j+1]) = (nums[j+1],nums[j]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }

            Console.WriteLine($"{string.Join(",",nums)}");
        }
    }
}
