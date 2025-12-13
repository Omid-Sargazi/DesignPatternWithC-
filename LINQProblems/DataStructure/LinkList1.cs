using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.DataStructure
{
    public class Node
    {
        public int Value = 0;
        public Node Next = null;

        public Node(int value)
        {
            Value = value;
            Next = null;
        }

    }
    public class LinkedLis1
    {
        public Node _head { get; set; }


        public LinkedLis1()
        {
            _head = null;
        }

       

        public void Add(int val)
        {
            Node newNode = new Node(val);
            if (_head == null)
            {
                _head = newNode;
                return;
            }

            Node cur = _head;
            while (cur.Next !=null)
            {
                cur = cur.Next;
            }
            cur.Next = newNode;
        }

        public void Print()
        {
            Node cur = _head;
            while (cur != null)
            {
                Console.WriteLine(cur.Value);
                cur = cur.Next;
            }
        }
    }

    public class RunLinkedList
    {
        public static void Run()
        {
            LinkedLis1 l = new LinkedLis1();
            for (int i = 0; i < 100; i++)
            {
                l.Add(i);
            }
            l.Print();

        }
    }
}
