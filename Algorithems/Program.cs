// See https://aka.ms/new-console-template for more information

using Algorithems.Sortings;

Console.WriteLine("Hello, World!");
int[] arr = new int[] {-7,7,-8,-9,8,9,4,1,10,12,41,10,45,-9,-8};
//Sorting1.Sorting.Bubble(arr);
//Sorting1.Sorting.Selection(arr);
//Sorting1.Sorting.Insertion(arr);
//Sorting1.Sorting.MergeSorting(arr);
int[] arr2 = new int[] { 1, 2, 3 };
Sorting1.Sorting.PivotDemo(arr,0,arr.Length-1);
Sorting2.Bubble(arr);

Sorting3.Run(arr2);