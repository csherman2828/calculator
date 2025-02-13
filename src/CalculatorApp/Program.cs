using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;
using CalculatorApp.Builder;
using CalculatorApp.Rules;

namespace CalculatorApp;

// the entry point of the String Calculator App
public class Program
{
  public static void Main(string[] args)
  {
    CalculatorArgs calculatorArgs = new(args);

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy(calculatorArgs.AlternativeDefaultDelim));

    OperandRules operandRules = new();
    if (!calculatorArgs.ShouldAllowNegatives)
    {
      operandRules.AddRule(new NoNegativesRule());
    }

    OperandTransformer operandTransformer = new();
    operandTransformer.AddTransformation(new UpperBoundTransformation(calculatorArgs.UpperBound));

    CalculatorBuilder calculatorBuilder = new();
    Calculator calculator = calculatorBuilder
      .SetOperation(calculatorArgs.Operation)
      .SetStringSplitter(stringSplitter)
      .SetStringToIntConverter(new StringToIntConverter())
      .SetOperandRules(operandRules)
      .SetOperandTransformer(operandTransformer)
      .Build();

    Describe(calculatorArgs.Operation);

    while (true)
    {
      string input = Prompt(calculatorArgs.Operation);
      string formula = calculator.DisplayFormula(input);
      Console.WriteLine(formula);
    }
  }

  private static void Describe(string operation)
  {
    string prompt = $"{operation.ToLower()}> ";

    Console.WriteLine("CalculatorApp");
    Console.WriteLine($"Type in 1 or 2 numbers separated by a comma to {prompt}\n");
    Console.WriteLine("Examples:");
    Console.WriteLine($"{prompt}1");
    Console.WriteLine($"{prompt}> 1,2\n");
    Console.WriteLine("------------------------\n");
  }

  private static string Prompt(string operation)
  {
    string prompt = $"{operation.ToLower()}> ";

    Console.Write(prompt);
    return Console.ReadLine() ?? string.Empty;
  }
}
