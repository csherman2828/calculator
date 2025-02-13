using CalculatorApp;

namespace CalculatorApp.Converters;

// this converter is used to convert a list of strings to a list of integers
// TODO: use int.TryParse instead of int.Parse to avoid exceptions
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
