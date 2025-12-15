using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeProblems.Problems03
{
    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }

        public Node(int val)
        {
            Value = val;
            Next = null;
        }
    }
    public class LinkedList
    {
        public Node _head { get; set; }
        public LinkedList(Node head)
        {
            _head = head;
        }

        public void Add(int val)
        {

        }
    }
}
