using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
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

        public bool Remove(int data)
        {
            if (headNode == null)
            {
                return false;
            }

            if (headNode.Data == data)
            {
                headNode = headNode.Next;
                return true;
            }


            var current = headNode;
            while (current.Next != null)
            {
                if (current.Next.Data == data)
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Reverse()
        {
            
            if (headNode == null)
            {
                return;
            }
            var current = headNode;
            Node prev = null;
            Node next = null;
            while (current !=null)
            {

                next = current.Next;//save next
                current.Next = prev;//change direction
                prev = current;//next [rev
                current = next;//next current
            }

            headNode = prev;

        }
    }
}
