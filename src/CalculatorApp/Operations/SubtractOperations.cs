namespace CalculatorApp.Operations;

public class SubtractOperation : ICalculatorOperation
{
  public int Identity => 0;

  public string Formulate(List<int> operands)
  {
    if (operands.Count == 0)
      return "0";
    return string.Join("-", operands);
  }

  public int Operate(int a, int b)
  {
    return a - b;
  }
}
