namespace CalculatorApp.Utils;

public class TooManyAddendsException : Exception
{
  public TooManyAddendsException() : base("More than two addends provided") { }
}

public class Calculator
{
  public int Calculate(string input)
  {
    List<int> addends = ConvertStringToIntList(input);

    int sum = 0;
    foreach (int addend in addends)
    {
      sum += addend;
    }
    return sum;
  }

  private List<int> ConvertStringToIntList(string input)
  {
    string[] delimiters = { ",", "\n" };

    // will be broken down eventually by each delimiter
    List<string> addendStrings = new List<string>();
    addendStrings.Add(input);

    // work through each delimiter and split the addend strings
    foreach (string delimiter in delimiters)
    {
      List<string> newAddendStrings = new List<string>();

      foreach (string addendString in addendStrings)
      {
        newAddendStrings.AddRange(addendString.Split(delimiter));
      }
      addendStrings = newAddendStrings;
    }

    // at this point, all addends are split by the delimiters, and we will try 
    //   to parse each one into an integer
    List<int> addends = new List<int>();
    foreach (string addendString in addendStrings)
    {
      try
      {
        int addend = int.Parse(addendString);
        addends.Add(addend);
      }
      catch (FormatException) { }
    }

    return addends;
  }
}
