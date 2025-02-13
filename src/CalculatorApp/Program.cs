using CalculatorApp.Utils;

namespace CalculatorApp;

public class CalculatorArgs
{
  private bool _shouldRejectNegatives;
  public CalculatorArgs(string[] args)
  {
    _shouldRejectNegatives = true; // default value

    foreach (string arg in args)
    {
      if (arg == "-n")
      {
        _shouldRejectNegatives = false;
      }
    }
  }

  public bool RejectNegatives
  {
    get
    {
      return _shouldRejectNegatives;
    }
  }
}

public class Program
{
  private const string PROMPT = "add> ";
  public static void Main(string[] args)
  {
    CalculatorArgs calculatorArgs = new(args);

    Calculator calculator = new(calculatorArgs.RejectNegatives);

    Describe();

    while (true)
    {
      string input = Prompt();
      string formula = calculator.DisplayFormula(input);
      Console.WriteLine(formula);
    }
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
