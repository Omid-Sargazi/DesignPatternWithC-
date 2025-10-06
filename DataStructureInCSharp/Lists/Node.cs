using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureInCSharp.Lists
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        

        public Node(int data)
        {
            Data = data;
            Next = null;
           
        }
    }

    public class LinkedList
    {
        private Node _head;
        public int Count;
        public LinkedList()
        {
            Count = 0;
        }

        public LinkedList(Node head)
        {
            _head = head;
            Count = 0;
        }


        public void Add(int value)
        {
            if (_head == null)
            {
                _head = new Node(value);
                Count++;
                
                return;
            }

            var current = _head;
            while (current.Next !=null)
            {
                current = current.Next;
            }

            current.Next = new Node(value);
            Count++;
        }

        public void Print()
        {
            if(_head==null) Console.WriteLine("List is Empty");
            var current = _head;
            while (current != null)
            {
                Console.Write(current.Data+"->");
                current = current.Next;
            }
        }

        public void Reverse()
        {
            if (_head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }

            var current = _head;
            Node prev = null;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            _head = prev;
        }
    }

    public class ClientLinkedList
    {
        public static void Run()
        {
            LinkedList l1 = new LinkedList();
            l1.Add(1);
            l1.Add(11);
            l1.Add(10);
            l1.Add(21);
            l1.Add(13);
            l1.Print();
            l1.Reverse();
            Console.WriteLine(" ");
            l1.Print();
        }
    }
}
