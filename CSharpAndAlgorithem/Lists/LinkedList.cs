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
    public class LinkedList
    {

    }

    public class CLientLinkedList
    {
        public static void Run()
        {
            Node n1 = new Node(1);
            Node n2 = new Node(2);
            Node n3 = new Node(3);
            Node n4 = new Node(4);

            n1.next = n2;
            n2.next = n3;
            n3.next = n4;
            Console.WriteLine(n1.Data+" "+ n1.next.Data);
        }
    }
}
