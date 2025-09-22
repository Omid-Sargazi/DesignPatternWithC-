using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Linked_List
{
    public class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
        }
    }

    public class LinkedList
    {
        private Node head;

        public LinkedList()
        {
            head = null;
        }

        public void AddLast(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next!=null)
                {
                    {
                        current = current.Next;
                    }
                    current.Next = newNode;
                }
            }
        }
    }
}
