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

  // The builder class is responsible for making the Calculator object by
  // injecting concrete implemnations of the interfaces into the Calculator.
  // This keeps Calculator free on coupling to concrete implementations
  // of its classes. It is a form of "Inversion of Control", where rather
  // than the Calculator depending outward on concrete implemenations,
  // it depends on its interfaces while the concrete implementations depend
  // on the Calculator for its interfaces.
  // 
  // This also counts as a rudimentary
  // form of "DependencyInjection", since we pass in dependencies into
  // the Calculator constructor at runtime rather than import concrete implementations
  // at compile time. While this is not done with an official DI or IoC library or
  // framework, it's a start while I learn the language.
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
        _operation = new AddOperation();
        break;
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