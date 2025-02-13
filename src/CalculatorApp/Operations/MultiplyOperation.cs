namespace CalculatorApp.Operations;

public class MultiplyOperation : ICalculatorOperation
{
  public int Identity => 1;
  public int Operate(int a, int b)
  {
    return a * b;
  }
}
