namespace CalculatorApp;

// any instance that implements IOperandRules is able to take a list of integer
// operands and enforce rules on them, throwing an exception if the rules are not
// satisfied
public interface IOperandRules
{
  public void Enforce(List<int> operands);
}