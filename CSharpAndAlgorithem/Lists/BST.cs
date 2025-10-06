using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAndAlgorithem.Lists
{
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    public class BST
    {
        private TreeNode _root;

        public BST()
        {
            _root = null;
        }

        public BST(int value)
        {
            _root = new TreeNode(value);
        }

        public void Insert(int value)
        {
            _root = InsertRec(_root, value);
        }

        public TreeNode InsertRec(TreeNode root, int value)
        {
            if (root == null)
            {
                root = new TreeNode(value);
                return root;
            }

            if (value < root.Value)
            {
                root.Left = InsertRec(root.Left, value);
                
            }
            else if(value>root.Value)
            {
                root.Right = InsertRec(root.Right, value);
            }

            return root;
        }

        



    }
}
