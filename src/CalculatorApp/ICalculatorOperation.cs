namespace CalculatorApp;

// each operation needs to define how to write the formula for the expression
// as well as how to perform the operation on two operands
public interface ICalculatorOperation
{
  public string Formulate(List<int> operands);
  public int Operate(int operandA, int operandB);
}