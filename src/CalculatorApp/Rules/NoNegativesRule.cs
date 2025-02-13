namespace CalculatorApp.Rules;

// when enforced, this rule will throw an exception if any of the operands are negative
public class NoNegativesRule : IOperandRule
{
  public void Enforce(List<int> operands)
  {
    List<int> negativeOperands = operands.Where(x => x < 0).ToList();
    if (negativeOperands.Count > 0)
    {
      throw new NegativeOperandException(negativeOperands);
    }
  }
}