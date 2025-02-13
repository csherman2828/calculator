using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;
using CalculatorApp.Rules;
using CalculatorApp.Operations;

namespace CalculatorApp.Builder;

public class CalculatorBuilder
{
  private ICalculatorOperation _operation;
  private IStringSplitter _stringSplitter;
  private IOperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;
  private IOperandRules _operandRules;

  public CalculatorBuilder()
  {
    // TODO: a bit messy/inconsistent on the defaults, would want to clean this up
    _operation = new AddOperation();
    _stringSplitter = StringSplitter.Default;
    _operandTransformer = OperandTransformer.Default;
    _stringToIntConverter = new StringToIntConverter();
    _operandRules = new OperandRules();
  }

  public CalculatorBuilder SetOperation(string operation)
  {
    switch (operation.ToLower())
    {
      case "add":
        _operation = new AddOperation();
        break;
      case "subtract":
        _operation = new SubtractOperation();
        break;
      case "multiply":
        _operation = new MultiplyOperation();
        break;
      case "divide":
        _operation = new DivideOperation();
        break;
      default:
        throw new ArgumentException($"Unknown operation: {operation}");
    }
    return this;
  }

  public CalculatorBuilder SetStringSplitter(StringSplitter stringSplitter)
  {
    _stringSplitter = stringSplitter;
    return this;
  }

  public CalculatorBuilder SetOperandTransformer(IOperandTransformer operandTransformer)
  {
    _operandTransformer = operandTransformer;
    return this;
  }

  public CalculatorBuilder SetStringToIntConverter(IStringToIntConverter stringToIntConverter)
  {
    _stringToIntConverter = stringToIntConverter;
    return this;
  }

  public CalculatorBuilder SetOperandRules(IOperandRules operandRules)
  {
    _operandRules = operandRules;
    return this;
  }

  public Calculator Build()
  {
    return new Calculator(
      _operation,
      _stringSplitter,
      _stringToIntConverter,
      _operandRules,
      _operandTransformer
    );
  }
}