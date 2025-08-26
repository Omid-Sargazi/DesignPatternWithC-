// See https://aka.ms/new-console-template for more information

using DataStructure.Patterns;
using DataStructure.Sorting;

Console.WriteLine("Hello, World!");
int[] arr = new[] { 70, -70, 4, 1, 2, 3, 41, -70, -80, -99, -1000};
BubbleSorting.RunBubble(arr);
Console.WriteLine(DivideAndConqure.Run(arr,0,arr.Length-1));

AdapterClient.Run();