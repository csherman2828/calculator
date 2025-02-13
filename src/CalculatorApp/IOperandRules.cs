namespace CalculatorApp;

public interface IOperandRules
{
  public void Enforce(List<int> operands);
}