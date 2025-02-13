namespace CalculatorApp;

// any instance of a class that implements IOperandTransformer is able to take
// a list of integers and transform them into another list of integers (changing
// none, some, or all of the values)
public interface IOperandTransformer
{
  public List<int> Transform(List<int> input);
}
