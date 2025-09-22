using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Linked_List
{
    public class CircularLinkedList
    {
        private Node tail;

        public CircularLinkedList()
        {
            tail = null;
        }

        public void AddLast(int data)
        {
            Node newNode = new Node(data);

            if (tail == null)
            {
                tail = newNode;
                tail.Next = tail;
            }
            else
            {
                newNode.Next = tail.Next;
                tail.Next = newNode;
                tail = newNode;
            }
        }

        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            if (tail == null)
            {
                tail = newNode;
                tail.Next = tail;
            }
            else
            {
                newNode.Next = tail.Next;
                tail.Next = newNode;
            }
        }
    }
}
