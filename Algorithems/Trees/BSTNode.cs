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
    }
}
