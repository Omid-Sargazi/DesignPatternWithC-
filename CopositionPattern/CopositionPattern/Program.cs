using CopositionPattern.CompositionPattern;
using CopositionPattern.DecoratorPattern;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");
        CompositeClient.Display();

        DisplayCompanyEmployee.Run();

        ClientCoffee.Run();
    }
}