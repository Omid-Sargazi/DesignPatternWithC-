using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.TreesInCSharp
{
    public class TreeNode
    {
        public string Value;
        public List<TreeNode> Children = new List<TreeNode>();

        public TreeNode(string value)
        {
            Value = value;
        }


        public void AddChild(TreeNode node)
        {
            Children.Add(node);
        }
    }

    public class RunTree
    {
        public static void Run()
        {
            var root = new TreeNode("Father");
            var child1 = new TreeNode("A1");
            var child2 = new TreeNode("B1");
            root.AddChild(child1);
            root.AddChild(child2);

            var childA1 = new TreeNode("a1");
            var childA12 = new TreeNode("a2");
            var childB1 = new TreeNode("b1");
            var childB12 = new TreeNode("b2");

            child1.AddChild(childA1);
            child1.AddChild(childA12);

            child2.AddChild(childB1);
            child2.AddChild(childB12);
            PrintTree(root,0);
        }

        private static void PrintTree(TreeNode root, int level=0)
        {
            Console.WriteLine(new string('-',level*2)+root.Value);
            foreach (var child in root.Children)
            {
                PrintTree(child,level+1);
            }
        }
    }
}
