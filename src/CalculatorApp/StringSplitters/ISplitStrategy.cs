namespace CalculatorApp.StringSplitters;

public interface ISplitStrategy
{
  public string Pattern
  {
    get;
  }

  List<string> Split(string input);
}
