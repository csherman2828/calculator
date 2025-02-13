namespace CalculatorApp.StringSplitters;

public class DefaultSplitStrategy : ISplitStrategy
{
  private List<string> _delimiters;
  public string Pattern => @".*";

  public DefaultSplitStrategy(string alternativeDelim = "\\n")
  {
    _delimiters = new List<string>
    {
      ",",
      alternativeDelim
    };
  }

  public List<string> Split(string input)
  {
    return input.Split(_delimiters.ToArray(), StringSplitOptions.None).ToList();
  }
}
