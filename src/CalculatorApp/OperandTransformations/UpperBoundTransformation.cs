namespace CalculatorApp.OperandTransformers;

public class UpperBoundTransformation : IOperandTransformation
{
  private const int DEFAULT_UPPER_BOUND = 1000;

  private int _upperBound;
  public UpperBoundTransformation(int upperBound = DEFAULT_UPPER_BOUND)
  {
    _upperBound = upperBound;
  }

  public int Transform(int input)
  {
    if (input > _upperBound)
    {
      return 0;
    }
    return input;
  }
}
