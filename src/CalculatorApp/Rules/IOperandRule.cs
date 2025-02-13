namespace CalculatorApp.Rules;

// an operand rule gets access to the whole list of operands and can decide
// to throw an exception if the rule is not satisfied
public interface IOperandRule
{
  void Enforce(List<int> operands);
}
