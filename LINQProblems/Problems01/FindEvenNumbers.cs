using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.Problems01
{
    public class FindEvenNumbers
    {
        public static void Run()
        {
            var nums1 = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var res1 = nums1.Where(n => n % 2 == 0).ToList();

        }
    }
}
