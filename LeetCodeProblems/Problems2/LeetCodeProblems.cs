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

        public static int BesTimeBuySellStock(int[] prices)
        {
            int minPrice = prices[0];
            int maxProfit = 0;
            for (int i = 1; i < prices.Length;i++)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }

                else
                {
                    int profit = prices[i] - minPrice;
                    if (profit > maxProfit)
                    {
                        maxProfit = profit;
                    }
                }
            }
            return maxProfit;
        }

        public bool IsValid(string s)
        {
            Dictionary<char, char> mapping = new Dictionary<char, char> {
                { ')', '(' },
                { '}', '{' },
                { ']', '[' }
            };

            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (mapping.ContainsValue(c))
                {
                    stack.Push(c);
                }
                else if(mapping.ContainsKey(c))
                {
                    if (stack.Count == 0 || stack.Pop() != mapping[c])
                    {
                        return false;
                    }
                }

            }

            return stack.Count == 0;
        }

        public bool IsValid2(string s)
        {
            Stack<char> stack = new Stack<char>();

            foreach (var c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count == 0) return false;

                    char top = stack.Pop();

                    if ((c == ')' && top != '(') ||
                        (c == ']' && top != '[') ||
                        (c == '}' && top != '{'))
                    {
                        return false;
                    }
                }
            }

            return stack.Count == 0;
        }


        public bool IsValid3(string s)
        {
            Stack<char> Seen = new Stack<char>();

            foreach (var c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    Seen.Push(c);
                }

                
                else
                {
                    if(Seen.Count==0) return false;

                    var top = Seen.Pop();

                    if ((c == ')' && top != '(') || (c == '}' && top != '{') || (c == ']' && top != '[')) return false;
                }
            }

            return Seen.Count == 0;
        }

    }
}
