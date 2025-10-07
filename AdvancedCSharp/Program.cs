//// See https://aka.ms/new-console-template for more information
//using AdvancedCSharp.Delegate;
//using System.Linq;
//using AdvancedCSharp.DataStructure;

//Console.WriteLine("Hello, World!");
////DelegateExample1.Run();
//DelegateExample1.DisplayMessage("hIII");

////===========================================
//MathOperation operation;

//operation = DelegateExample2.Add;
//operation(4, 5);

//operation = DelegateExample2.Subtract;
//operation(60, 10);

//operation = DelegateExample2.Minus;
//operation(10, 5);


//int[] nums = { -2, -1, 0, 1, 2, 3, 4 };

//NumberChecker isEvent = DelegateExample3.IsEvent;

//DelegateExample3.CheckNumbers(nums, isEvent);

//Action<string> greetDelegate = DelegateExample4.ShowMessage;
//greetDelegate("Hi From Action Delegate");


//Func<int, int, int> add = DelegateExample5.Add;
//int result = add(4, 5);
//Console.WriteLine(result);



//static void ShowMessage(string text)=>Console.WriteLine(text);
//Button button = new Button();
//button.OnClick += ShowMessage;
//button.Click("Hii Saeed");

//Func<int, int> doubleOp = DelegateExample6.Double;
//Func<int, int> squareOp = DelegateExample6.Square;

//Func<int, int> doubleThenSquare = x => squareOp(doubleOp(x));
//int resultt = doubleThenSquare(3);
//Console.WriteLine(resultt);

//RunCal.Run(5,5,"Add");

//int[] arr = new[] { 70, -70, 10, -10, 40, 1, 2, 3, 4, 4, 3, 2, 1, 0, 0, 0 };
////BubbleSorting.Run(arr);
////SelectionSort.Run(arr);
////InsertionSort.Run(arr);

//int[] arr2 = new[] { 1, 2, 3,4,5 };
////Console.WriteLine(SumDivideConquer.Run(arr2, 0, 4));
//Console.WriteLine(SumDivideConquer.PowerDivideConquer(3,3));


////===========================================


using AdvancedCSharp.DataStructure2;
using AdvancedCSharp.LeetCode;
using AdvancedCSharp.Sorting;

Console.WriteLine("LeetCode");
var nums = new int[]{3,4,5,6,0,1,2};
//Console.WriteLine(MinInRotatedArray.Run(nums));

//Bubble.RunBubble(nums);

Sortings.Bubble(nums);