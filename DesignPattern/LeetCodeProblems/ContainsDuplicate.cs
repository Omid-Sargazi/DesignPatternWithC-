using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.LeetCodeProblems
{
    public class ContainsDuplicate
    {
        public bool RunContainsDuplicate(int[] nums)
        {
            var seen = new HashSet<int>();
            foreach(int num in nums)
            {
                if(seen.Contains(num))
                    return true;
                seen.Add(num);
            }
            return false;
        }
    }
}
