namespace CalculatorApp.Rules;

public interface IOperandRule
{
  void Enforce(List<int> operands);
}
