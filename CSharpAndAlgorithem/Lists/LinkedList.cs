using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.Lists
{
    public class Node
    {
        public int Data { get; set; }
        public Node next { get; set; }

        public Node(int data)
        {
            Data = data;
            next = null;
        }
    }
    public class LinkedListt
    {
        private Node head;
        public LinkedListt(Node node)
        {
            head = node;
        }

        public LinkedListt()
        {

        }

        public  void Add(Node node)
        {
            
            if (head == null)
            {
                head = node;
                return;
            }
            var current = head;
            while (current.next !=null)
            {
                current = current.next;
            }

            current.next = node;
        }

        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("List Is Empty");
                return;
            }

            var current = head;
            while (current != null)
            {
                Console.Write(current.Data+"->");
                current = current.next;
            }
            Console.WriteLine("null");
        }
    }

    public class CLientLinkedList
    {
        public static void Run()
        {
            LinkedListt l1 = new LinkedListt();
            l1.Add(new Node(1));

            l1.Add(new Node(2));
            l1.Print();
        }
    }
}
