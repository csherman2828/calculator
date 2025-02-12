namespace CalculatorApp.Utils;

public class TooManyAddendsException : Exception
{
  public TooManyAddendsException() : base("More than two addends provided") { }
}

public class NegativeAddendException : Exception
{
  List<int> negativeAddends;
  public NegativeAddendException(List<int> negativeAddends) : base($"Negative addends provided: {string.Join(", ", negativeAddends)}")
  {
    this.negativeAddends = negativeAddends;
  }
  public List<int> GetNegativeAddends => negativeAddends;
}

public class Calculator
{
  private const int DELIMITER_POSITION = 2;
  private const int ADDEND_STRING_START = 5;

  public int Calculate(string input)
  {
    string originalAddendString;
    List<string> delimiters = new List<string> { ",", "\\n" };

    if (input.StartsWith("//"))
    {
      char customDelimiter = input[DELIMITER_POSITION];
      delimiters.Add(customDelimiter.ToString());
      originalAddendString = input.Substring(ADDEND_STRING_START);
    }
    else
    {
      originalAddendString = input;
    }

    List<int> addends = ConvertStringToIntList(originalAddendString, delimiters)
      .Where(addend => addend <= 1000).ToList();


    AssertNoNegatives(addends);

    int sum = 0;
    foreach (int addend in addends)
    {
      sum += addend;
    }
    return sum;
  }

  private List<int> ConvertStringToIntList(string input, List<string> delimiters)
  {

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

  private void AssertNoNegatives(List<int> addends)
  {
    List<int> negativeAddends = new List<int>();
    foreach (int addend in addends)
    {
      if (addend < 0)
      {
        negativeAddends.Add(addend);
      }
    }
    if (negativeAddends.Count > 0)
    {
      throw new NegativeAddendException(negativeAddends);
    }
  }
}
