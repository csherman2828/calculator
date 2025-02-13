namespace CalculatorApp;

public interface ICalculatorOperation
{
  public int Identity { get; }
  public int Operate(int a, int b);
}