namespace CalculatorApp.Rules;

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