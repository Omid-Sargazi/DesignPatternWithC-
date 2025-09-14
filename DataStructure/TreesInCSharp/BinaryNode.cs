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

        private static void Inorder(BinaryNode root)
        {
            if(root==null) return;
            Inorder(root.left);
            Console.WriteLine(root.Value+" ");
            Inorder(root.right);
        }

        private static void Postorder(BinaryNode root)
        {
            if(root == null) return;
            Postorder(root.left);
            Postorder(root.right);
            Console.WriteLine(root.Value+" ");
        }

        public static void LevelOrder(BinaryNode root)
        {
            if(root==null) return;

            var q = new Queue<BinaryNode>();
            q.Enqueue(root);
            while (q.Count>0)
            {
                var cur = q.Dequeue();
                Console.WriteLine(cur.Value);
                if(cur.left !=null) q.Enqueue(cur.left);
                if(cur.right !=null) q.Enqueue(cur.right);
            }
        }
    }
}
