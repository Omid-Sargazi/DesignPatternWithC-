using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problem1
{
    public class CountNumbersInArray
    {
        public static Dictionary<int,int> Run(int[] nums)
        {
            var freq = new Dictionary<int, int>();
            foreach(var num in nums)
            {
                if(!freq.ContainsKey(num))
                    freq[num] = 1;
                freq[num]++;
            }

            return freq;
        }

        public static Dictionary<string, int> RunCountString(string[] words)
        {
            var freq = new Dictionary<string, int>();
            foreach(var word in words)
            {
                if(freq.ContainsKey(word))
                    freq[word]++;
                freq[word] =1;
            }

            return freq;
        }
    }
}
