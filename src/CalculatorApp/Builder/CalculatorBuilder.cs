using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;
using CalculatorApp.Rules;

namespace CalculatorApp.Builder;

public class CalculatorBuilder
{
  private IStringSplitter _stringSplitter;
  private IOperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;
  private IOperandRules _operandRules;

  public CalculatorBuilder()
  {
    // TODO: a bit messy/inconsistent on the defaults, would want to clean this up
    _stringSplitter = StringSplitter.Default;
    _operandTransformer = OperandTransformer.Default;
    _stringToIntConverter = new StringToIntConverter();
    _operandRules = new OperandRules();
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
      _stringSplitter,
      _stringToIntConverter,
      _operandRules,
      _operandTransformer
    );
  }
}