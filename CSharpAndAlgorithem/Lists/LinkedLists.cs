using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.Lists
{
    public class CustomeNode
    {
        public int Data { get; set; }
        public CustomeNode Next { get; set; }

        public CustomeNode(int data)
        {
            Next = null;
            Data = data;

        }
    }
    public class LinkedLists
    {
        private CustomeNode _head;
        public LinkedLists()
        {
            
        }

        public LinkedLists(CustomeNode head)
        {
            _head = head;
        }

        public void Add(int value)
        {
            if (_head == null)
            {
                _head = new CustomeNode(value);
                return;
            }
            var current = _head;
            while (current.Next !=null)
            {
                current = current.Next;
            }

            current.Next = new CustomeNode(value);
        }

        public void Reverse()
        {
            if (_head == null)
            {
                Console.WriteLine("List is Empty");
                return;
            }

            var current = _head;
            CustomeNode prev = null;
            CustomeNode next = null;

            while (current !=null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            _head = prev;
        }

        public void AddFirst(int value)
        {
            CustomeNode newNode = new CustomeNode(value);
            if (_head == null)
            {
                _head = newNode;
                return;
            }

            
            newNode.Next = _head.Next;
            _head = newNode;

        }

        public void AddLastList(int value)
        {
            CustomeNode newNode = new CustomeNode(value);
            if (_head == null)
            {
                _head = newNode;
                return;
            }

            var current = _head;
            while (current.Next !=null)
            {
                
                    current = current.Next;
                    
            }
            current.Next = newNode;
        }

        public void Print()
        {
            if (_head == null)
            {
                Console.WriteLine("List is Empty");
                return;
            }
            var current = _head;
            while (current !=null)
            {
                Console.Write(current.Data+"->");
                current = current.Next;
            }
        }
    }

    public class CLientLists
    {
        public static void Run()
        {
            LinkedLists l1 = new LinkedLists();
            l1.Add(1);
            l1.Add(10);
            l1.Add(11);
            l1.AddFirst(0);
            l1.AddLastList(1000);
            l1.Add(12);
            l1.Print();
            l1.Reverse();
            Console.WriteLine("Reverse");
            l1.Print();
        }
    }
}
