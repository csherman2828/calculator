using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Builder;
using CalculatorApp.Rules;

namespace CalculatorApp;

public class Program
{
  private const string PROMPT = "add> ";
  public static void Main(string[] args)
  {
    CalculatorArgs calculatorArgs = new(args);

    CalculatorBuilder calculatorBuilder = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
    calculatorBuilder.SetStringSplitter(stringSplitter);

    OperandRules operandRules = new();

    if (!calculatorArgs.ShouldAllowNegatives)
    {
      operandRules.AddRule(new NoNegativesRule());
    }

    calculatorBuilder.SetOperandRules(operandRules);

    OperandTransformer operandTransformer = new();
    operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
    calculatorBuilder.SetOperandTransformer(operandTransformer);

    Calculator calculator = calculatorBuilder.Build();

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
