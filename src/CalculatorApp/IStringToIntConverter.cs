namespace CalculatorApp;

// This interface is used to convert a list of strings into a list of integers.
public interface IStringToIntConverter
{
  public List<int> Convert(List<string> input);
}