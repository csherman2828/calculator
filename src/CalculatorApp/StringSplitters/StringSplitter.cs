using System.Text.RegularExpressions;

namespace CalculatorApp.StringSplitters;

// the StringSplitter instance is responsible for splitting a string into its components
// there are several strategies that can be applied, such as parsing the beginning
// of the string for a list of multiple strings to use as delimiters.
//
// StringSplitter will collect a list of split strategies and apply them in order
// Each split strategy is checked to see if the input string matches the
// Regex pattern. If a match is found, the split strategy is applied to derive
// a list of split token strings
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
