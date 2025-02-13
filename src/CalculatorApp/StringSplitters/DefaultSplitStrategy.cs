namespace CalculatorApp.StringSplitters;

public class DefaultSplitStrategy : ISplitStrategy
{
  private readonly string[] DEFAULT_DELIMITERS = { ",", "\\n" };
  public string Pattern => @".*";

  public List<string> Split(string input)
  {
    return input.Split(DEFAULT_DELIMITERS, StringSplitOptions.None).ToList();
  }
}
