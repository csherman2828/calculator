using System.Text.RegularExpressions;

namespace CalculatorApp.StringSplitters;

public class StringSplitter : IStringSplitter
{
  private List<ISplitStrategy> _splitStrategies = new List<ISplitStrategy>();

  public void AddSplitStrategy(ISplitStrategy splitStrategy)
  {
    _splitStrategies.Add(splitStrategy);
  }

  public List<string> Split(string input)
  {
    foreach (var splitStrategy in _splitStrategies)
    {
      if (Regex.IsMatch(input, splitStrategy.Pattern))
      {
        return splitStrategy.Split(input);
      }
    }
    throw new ArgumentException("No matching split strategy found.");
  }

  public static StringSplitter Default
  {
    get
    {
      StringSplitter stringSplitter = new StringSplitter();
      stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
      return stringSplitter;
    }
  }
}
