namespace CalculatorApp;

public interface IOperandTransformer
{
  public List<int> Transform(List<int> input);
}
