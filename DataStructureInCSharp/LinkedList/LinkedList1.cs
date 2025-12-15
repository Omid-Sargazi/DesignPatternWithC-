using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructureInCSharp.Lists;

namespace DataStructureInCSharp.LinkedList
{
    public class Node1
    {
        public int Value = 0;
        public Node1 Next = null;

        public Node1(int value)
        {
            Value = value;
            Next = null;
        }
    }
    public class LinkedList1
    {
        public Node1 _head { get; set; }

        public LinkedList1()
        {
            _head = null;
        }

        public void Add(int val)
        {
            Node1 newNode = new Node1(val);
            if (_head == null)
            {
                _head = newNode;
            }

            Node1 cur = _head;

            while (cur.Next !=null)
            {
                cur =cur.Next;
            }

            cur.Next = newNode;
        }

        public void Print()
        {
            Node1 cur = _head;

            while (cur !=null)
            {
                Console.WriteLine(cur.Value);
                cur = cur.Next;
            }
        }
    }
}
