namespace CalculatorApp.Utils;

public class TooManyAddendsException : Exception
{
  public TooManyAddendsException() : base("More than two addends provided") { }
}

public class Calculator
{
  public int Calculate(string input)
  {
    List<string> addendStrings = ConvertStringToIntList(input);

    int sum = 0;
    foreach (string addendString in addendStrings)
    {
      try
      {
        int addend = int.Parse(addendString);
        sum += addend;
      }
      catch (FormatException) { }
    }
    return sum;
  }

  private List<string> ConvertStringToIntList(string input)
  {
    return input.Split(',').ToList();
  }
}
