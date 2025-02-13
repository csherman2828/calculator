namespace CalculatorApp;

// Responsible for storing two artifacts after a calculation is performed: the
// answer (an integer solution) and a formula (a string representation of all 
// the operands separated by the operator and equal to the answer)
public class CalculatorResult
{
  private int _answer;
  private string _formula;

  public CalculatorResult(int answer, string formula)
  {
    _answer = answer;
    _formula = formula;
  }

  public int Answer
  {
    get { return _answer; }
  }

  public string Formula
  {
    get { return _formula; }
  }
}