namespace CalculatorApp.Operations;

public class AddOperation : ICalculatorOperation
{
  public int Identity => 0;
  public int Operate(int a, int b)
  {
    return a + b;
  }
}
