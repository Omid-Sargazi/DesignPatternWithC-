// See https://aka.ms/new-console-template for more information
using AdvancedCSharp.Delegate;

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

//===========================================
