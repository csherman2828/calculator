namespace CalculatorApp.Rules;

// this is the exception thrown when a negative operand is encountered if a rule
// to deny negative numbers is enforced.
//
// it contains a list of the negative addends that were encountered as a member
// variable and in the exception message
public class NegativeOperandException : Exception
{
  private List<int> _negativeAddends;

  public NegativeOperandException(List<int> negativeAddends) : base($"Negative addends provided: {string.Join(", ", negativeAddends)}")
  {
    _negativeAddends = negativeAddends;
  }
  public List<int> NegativeAddends => _negativeAddends;
}