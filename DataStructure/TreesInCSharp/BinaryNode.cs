using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.TreesInCSharp
{
    public class BinaryNode
    {
        public int Value;
        public BinaryNode left;
        public BinaryNode right;

        public BinaryNode(int value)
        {
            Value = value;
        }
    }

    public class RunBinaryNode
    {
        public static void Run()
        {
            var root = new BinaryNode(1);
            root.left = new BinaryNode(2);
            root.right = new BinaryNode(3);
            root.left.left  = new BinaryNode(4);
            root.left.right = new BinaryNode(5);
            root.right.left = new BinaryNode(6);
            root.right.right = new BinaryNode(7);
        }

        private static void Preorder(BinaryNode root)
        {
            if(root==null) return;
            Console.WriteLine(root.Value);
            Preorder(root.left);
            Preorder(root.right);
        }
    }
}
