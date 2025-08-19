// See https://aka.ms/new-console-template for more information
using DesignPattern.MediatorPattern;
using DesignPattern.ObserverPattern;
using DesignPattern.Sorting;
using DesignPattern.StrategyPattern;

Console.WriteLine("Hello, World!");
//ClientMediator.Run();
//ClientTax.Run();
//ClientObserver.Run();
int[] arr = new int[]{70,-40,10,1,1,2,3,0,70,-90,12};
//BubbleSorting.Run(arr);
//InsertionSort.Run(arr);
Selection.Run(arr);