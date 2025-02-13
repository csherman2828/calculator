namespace CalculatorApp.Rules;

public class NegativeOperandException : Exception
{
  private List<int> _negativeAddends;

  public NegativeOperandException(List<int> negativeAddends) : base($"Negative addends provided: {string.Join(", ", negativeAddends)}")
  {
    _negativeAddends = negativeAddends;
  }
  public List<int> NegativeAddends => _negativeAddends;
}