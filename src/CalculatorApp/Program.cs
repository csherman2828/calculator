using CalculatorApp.Utils;

namespace CalculatorApp;

public class Program
{
  const string PROMPT = "add> ";
  public static void Main()
  {
    Calculator calculator = new();

    Describe();

    string input = Prompt();

    string formula = calculator.DisplayFormula(input);

    Console.WriteLine(formula);
  }

  private static void Describe()
  {
    Console.WriteLine("CalculatorApp");
    Console.WriteLine("Type in 1 or 2 numbers separated by a comma to calculate their sum\n");
    Console.WriteLine("Examples:");
    Console.WriteLine($"{PROMPT}1");
    Console.WriteLine($"{PROMPT}1,2\n");
    Console.WriteLine("------------------------\n");
  }

  private static string Prompt()
  {
    Console.Write(PROMPT);
    return Console.ReadLine() ?? string.Empty;
  }
}
