using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.LeetCodeProblems
{
    public class LongestSubstringWithoutRepeatingChar
    {
        public static int Run(string s)
        {
            HashSet<char> seen = new HashSet<char>();
            int left = 0;
            int longest = 0;
            int right = 0;
            for (right = 0; right < s.Length; right++)
            {
                if (seen.Contains(s[right]))
                {
                    seen.Remove(s[left]);
                    left++;
                }

                seen.Add(s[right]);
                longest = Math.Max(longest, right - left+1);
            }

            

            return longest;
        }

        public static int SubArray(int[] arr, int k)
        {
            int left = 0;
            int max = 0;
            int windoSum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                windoSum += arr[i];
                if (i >= k - 1)
                {
                    max = Math.Max(max, windoSum);
                    windoSum -= arr[left];
                    left++;
                }
            }

            return max;
        }
    }
}
