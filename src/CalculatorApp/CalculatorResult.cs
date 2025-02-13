namespace CalculatorApp;

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