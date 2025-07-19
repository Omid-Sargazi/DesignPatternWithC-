using CopositionPattern.CompositionPattern;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello");
        CompositeClient.Display();

        DisplayCompanyEmployee.Run();
    }
}