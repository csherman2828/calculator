namespace CalculatorApp;

public interface ICalculatorOperation
{
  public string Formulate(List<int> operands);
  public int Operate(int a, int b);
}