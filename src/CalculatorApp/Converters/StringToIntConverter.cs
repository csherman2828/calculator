using CalculatorApp;

namespace CalculatorApp.Converters;

public class StringToIntConverter : IStringToIntConverter
{
  public List<int> Convert(List<string> inputs)
  {
    List<int> results = new List<int>();
    foreach (string input in inputs)
    {
      try
      {
        results.Add(int.Parse(input));
      }
      catch (FormatException)
      {
        results.Add(0);
      }
    }
    return results;
  }
}
