namespace CalculatorApp.Rules;

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