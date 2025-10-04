using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndLeetCode.Lists
{
    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }
    public class BST
    {

        private TreeNode _root = null;

        public BST(TreeNode parrent)
        {
            _root = parrent;
        }
        public BST(){}

        public void Insert(int value)
        {
            InsertInTree(_root, value);
        }

        private TreeNode InsertInTree(TreeNode node, int value)
        {
            if (node == null)
            {
                return new TreeNode(value);
            }

            if (node.Data > value)
            {
                node.Left = InsertInTree(node.Left, value);
            }

            if (node.Data < value)
            {
                node.Right = InsertInTree(node.Right, value);
            }

            return node;
        }
    }

    public class ClientTree
    {
        public static void Run()
        {
            BST b = new BST(new TreeNode(1) );
            b.Insert(10);
            b.Insert(-5);
            b.Insert(3);
            b.Insert(4);
            b.Insert(10);
            b.Insert(11);
           
        }
    }
}
