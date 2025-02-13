using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;
using CalculatorApp.Builder;
using CalculatorApp.Rules;

namespace CalculatorApp;

public class Program
{
  private const string PROMPT = "add> ";
  public static void Main(string[] args)
  {
    CalculatorArgs calculatorArgs = new(args);

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

    OperandRules operandRules = new();
    if (!calculatorArgs.ShouldAllowNegatives)
    {
      operandRules.AddRule(new NoNegativesRule());
    }

    OperandTransformer operandTransformer = new();
    operandTransformer.AddTransformation(new UpperBoundTransformation(calculatorArgs.UpperBound));

    CalculatorBuilder calculatorBuilder = new();
    Calculator calculator = calculatorBuilder
      .SetStringSplitter(stringSplitter)
      .SetStringToIntConverter(new StringToIntConverter())
      .SetOperandRules(operandRules)
      .SetOperandTransformer(operandTransformer)
      .Build();

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
