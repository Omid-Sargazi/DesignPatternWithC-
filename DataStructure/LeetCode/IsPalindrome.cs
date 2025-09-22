using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.LeetCode
{
    public class IsPalindrome
    {
        public static bool Run(string s)
        {
            if (string.IsNullOrEmpty(s)) return true;

            int left = 0;
            int right = s.Length - 1;

            while (left<right)
            {
                if (!char.IsLetterOrDigit(s[left]))
                    left++;
                else if (!char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }
                else
                {
                    if (char.ToLower(s[left]) != char.ToLower(s[right]))
                    {
                        left++;
                        right--;
                    }
                }
            }

            return true;
        }
    }
}
