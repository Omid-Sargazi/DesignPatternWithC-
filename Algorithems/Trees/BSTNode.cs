using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Trees
{
    public class BSTNode
    {
        public int Data;
        public BSTNode Left;
        public BSTNode Right;

        public BSTNode(int data)
        {
            Data = data;
            Left = Right = null;
        }
    }

    public class BinarySearchTree
    {
        private BSTNode root;

        public void Insert(int data)
        {
            root = InsertRec(root, data);
        }

        private BSTNode InsertRec(BSTNode node, int data)
        {
            if (node == null) return new BSTNode(data);

            if(data<node.Data)
                node.Left = InsertRec(node.Left, data);
            else if(data>node.Data)
                node.Right = InsertRec(node.Right, data);
            return node;
        }

        public bool Search(int data)
        {
            return SearchRec(root, data);
        }

        private bool SearchRec(BSTNode node, int data)
        {
            if(node==null) return false;

            if(data == node.Data) return true;

            return data < node.Data ? 
                SearchRec(node.Left, data) : 
                SearchRec(node.Right, data);
        }

        public void InOrder()
        {
            Console.Write("InOrder Traversal: ");
            InOrderRec(root);
            Console.WriteLine();
        }

        private void InOrderRec(BSTNode node)
        {
            if (node != null)
            {
                InOrderRec(node.Left);
                Console.WriteLine(node.Data+" ");
                InOrderRec(node.Right);
            }
        }

        public int FindMin()
        {
            if (root == null) throw new InvalidOperationException("This is Empty");
            return FindMinRec(root);
        }

        private int FindMinRec(BSTNode node)
        {
            return node.Left == null ? node.Data : FindMinRec(node.Left);
        }

        public int FindMax()
        {
            if (root == null) throw new InvalidOperationException("This is empty");
            return FindMaxRec(root);
        }

        private int FindMaxRec(BSTNode node)
        {
            return node.Right == null ? node.Data : FindMaxRec(node.Right);
        }

        public int Height()
        {
            return HeightRec(root);
        }

        private int HeightRec(BSTNode node)
        {
            if (node == null) throw new InvalidOperationException("This is Empty");
            return 1 + Math.Max(HeightRec(node.Left), HeightRec(node.Right));
        }
    }

    public class ClientTrre
    {
        public static void Run()
        {
            BinarySearchTree bst = new BinarySearchTree();

            int[] values = { 50, 30, 70, 20, 40, 60, 80 };
            foreach (var val in values)
            {
               bst.Insert(val); 
            }

            bst.InOrder();
        }
    }
}
