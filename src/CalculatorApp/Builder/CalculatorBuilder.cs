using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;

namespace CalculatorApp.Builder;

public class CalculatorBuilder
{
  private bool _shouldAllowNegatives;
  private IStringSplitter _stringSplitter;
  private IOperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;

  public CalculatorBuilder()
  {
    // TODO: a bit messy/inconsistent on the defaults, would want to clean this up
    _shouldAllowNegatives = false;
    _stringSplitter = StringSplitter.Default;
    _operandTransformer = OperandTransformer.Default;
    _stringToIntConverter = new StringToIntConverter();
  }

  public CalculatorBuilder AllowNegatives()
  {
    _shouldAllowNegatives = true;
    return this;
  }

  public CalculatorBuilder SetStringSplitter(StringSplitter stringSplitter)
  {
    _stringSplitter = stringSplitter;
    return this;
  }

  public CalculatorBuilder SetOperandTransformer(OperandTransformer operandTransformer)
  {
    _operandTransformer = operandTransformer;
    return this;
  }

  public CalculatorBuilder SetStringToIntConverter(IStringToIntConverter stringToIntConverter)
  {
    _stringToIntConverter = stringToIntConverter;
    return this;
  }

  public Calculator Build()
  {
    return new Calculator(
      _stringSplitter,
      _stringToIntConverter,
      _operandTransformer,
      _shouldAllowNegatives
    );
  }
}