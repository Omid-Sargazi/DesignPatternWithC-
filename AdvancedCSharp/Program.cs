// See https://aka.ms/new-console-template for more information
using AdvancedCSharp.Delegate;
using System.Linq;

Console.WriteLine("Hello, World!");
//DelegateExample1.Run();
DelegateExample1.DisplayMessage("hIII");

//===========================================
MathOperation operation;

operation = DelegateExample2.Add;
operation(4, 5);

operation = DelegateExample2.Subtract;
operation(60, 10);

operation = DelegateExample2.Minus;
operation(10, 5);


int[] nums = { -2, -1, 0, 1, 2, 3, 4 };

NumberChecker isEvent = DelegateExample3.IsEvent;

DelegateExample3.CheckNumbers(nums, isEvent);

Action<string> greetDelegate = DelegateExample4.ShowMessage;
greetDelegate("Hi From Action Delegate");


//===========================================
