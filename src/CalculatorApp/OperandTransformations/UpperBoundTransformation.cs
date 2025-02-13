namespace CalculatorApp.OperandTransformers;

// This transformer will take any number greater than the upper bound and turn it into 0 
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
