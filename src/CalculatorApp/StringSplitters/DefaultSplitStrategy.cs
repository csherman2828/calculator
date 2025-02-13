namespace CalculatorApp.StringSplitters;

// this split strategy parses a string with a comma and one alternate. By default,
// that alternate delimiter is a newline, but that can be changed with the constructor.
// in the context of our program this is the default, or fallback, strategy
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
