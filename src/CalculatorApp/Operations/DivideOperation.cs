namespace CalculatorApp.Operations;

public class DivideOperation : ICalculatorOperation
{
  public int Identity => 1;
  public int Operate(int a, int b)
  {
    return a / b;
  }
}
