namespace CalculatorApp.Rules;

// OperandRules instances enforce rules on a list of operands.
// The return value of "Enforce" is void - convention is to throw an exception
// if the rule is not satisfied.
public class OperandRules : IOperandRules
{
  private List<IOperandRule> _operandRules;

  public OperandRules()
  {
    _operandRules = new List<IOperandRule>();
  }

  public void AddRule(IOperandRule rule)
  {
    _operandRules.Add(rule);
  }

  public void Enforce(List<int> operands)
  {
    foreach (var rule in _operandRules)
    {
      rule.Enforce(operands);
    }
  }
}