using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.TreesInCSharp
{
    public class UniversityTree
    {
        public string Name { get; set; }
        public List<UniversityTree> Childern = new List<UniversityTree>();

        public UniversityTree(string name)
        {
            Name = name;
        }

        public void AddChild(UniversityTree child)
        {
            Childern.Add(child);
        }
    }

    public class RunUniversity
    {
        public static void Run()
        {
            var university = new UniversityTree("university");

            var engineering = new UniversityTree("engineering ");
            var medicine = new UniversityTree("medicine ");

            university.AddChild(engineering);
            university.AddChild(medicine);

            var computer = new UniversityTree("Computer Engineering");
            var electrical = new UniversityTree("Electrical Engineering");

            engineering.AddChild(computer);
            engineering.AddChild(electrical);

            UniversityTree pharmacy = new UniversityTree("Pharmacy");
            UniversityTree dentistry = new UniversityTree("Dentistry");

            medicine.AddChild(pharmacy);
            medicine.AddChild(dentistry);

        }

        private static void PrintTree(UniversityTree root,int level=0)
        {
            Console.WriteLine(new string('-',level*2)+root.Name);
            foreach (var child in root.Childern)
            {
                PrintTree(child,level+1);
            }
        }
    }
}
