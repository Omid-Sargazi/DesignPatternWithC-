using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndLeetCode.Lists
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }
    public class LinkedList
    {
        private Node headNode;
        public LinkedList()
        {
            
        }

        public LinkedList(Node node)
        {
            headNode = node;
        }

        public void Add(Node node)
        {
            if (headNode == null)
            {
                if (headNode == null)
                {
                    headNode = node;
                    return;
                }
            }

            var current = headNode;

                while (current.Next !=null)
                {
                    current = current.Next;
                }

                current.Next = node;
        }

        public void Print()
        {
            if (headNode == null)
            {
                Console.WriteLine("List is Empty");
                return;
            }

            var current = headNode;
            while (current.Next != null)
            {
                Console.WriteLine(current.Data+"->");
                current = current.Next;
            }
            Console.WriteLine("null");

        }
    }
}
