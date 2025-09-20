using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.TreesInCSharp
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

        public BSTNode InsertRec(BSTNode node, int data)
        {
            if (node == null) return new BSTNode(data);

            if (data < node.Data) node.Left = InsertRec(node.Left, data);
            else if (data > node.Data) node.Right = InsertRec(node.Right, data);
            return node;
        }

        public bool Search(int data)
        {
            return SearchRec(root, data);
        }

        public bool SearchRec(BSTNode node, int data)
        {
            if(node==null) return false;

            if(data==node.Data) return true;

            return data < node.Data ? 
                SearchRec(node.Left, data) : 
                SearchRec(node.Right, data);
        }
    }
}
