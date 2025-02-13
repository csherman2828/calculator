using CalculatorApp;
using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;

namespace CalculatorApp;

public class CalculatorArgs
{
  private bool _shouldAllowNegatives;
  public CalculatorArgs(string[] args)
  {
    _shouldAllowNegatives = false;

    foreach (string arg in args)
    {
      if (arg == "-n")
      {
        _shouldAllowNegatives = true;
      }
    }
  }

  public bool ShouldAllowNegatives
  {
    get
    {
      return _shouldAllowNegatives;
    }
  }
}

public class Program
{
  private const string PROMPT = "add> ";
  public static void Main(string[] args)
  {
    CalculatorArgs calculatorArgs = new(args);

    Calculator calculator = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
    calculator.SetStringSplitter(stringSplitter);

    if (calculatorArgs.ShouldAllowNegatives)
    {
      calculator.AllowNegatives();
    }

    OperandTransformer operandTransformer = new();
    operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
    calculator.SetOperandTransformer(operandTransformer);

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
