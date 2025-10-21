using SolvingProblemWithCSharp.Sorting;

namespace SolvingProblemWithCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] arr = new int[] { 1, -2, 30,28,0,-1,-7,7,8,10 };
            //CommonSorting.BubbleSort(arr);
            CommonSorting.SelectionSort(arr);
        }
    }
}
