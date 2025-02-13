using System;
using System.Text.RegularExpressions;

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
  public List<int> NegativeAddends => negativeAddends;
}

public class Calculator
{
  private const int DELIMITER_POSITION = 2;
  private const int ADDEND_STRING_START = 5;
  private const string SINGLE_CUSTOM_DELIMITER_REGEX = @"^//.\\n.*";
  private const string MULTI_CUSTOM_DELIMITER_REGEX = @"^//(\[[^\]]*\])+\\n.*";

  public int Calculate(string input)
  {
    List<string> addendStrings = _Split(input);
    List<int> potentialAddends = _Convert(addendStrings);
    _AssertNoNegatives(potentialAddends);
    List<int> addends = _Transform(potentialAddends);
    int result = _Calculate(addends);
    return result;
  }

  public string DisplayFormula(string input)
  {
    // this duplicated code makes me sad but we will make it right later
    List<string> addendStrings = _Split(input);
    List<int> potentialAddends = _Convert(addendStrings);
    _AssertNoNegatives(potentialAddends);
    List<int> addends = _Transform(potentialAddends);
    string formulaStart = string.Join("+", addends);
    int result = _Calculate(addends);
    string resultString = string.Join(" = ", new string[] { formulaStart, result.ToString() });
    return resultString;
  }

  private List<string> _Split(string input)
  {
    string originalAddendString;
    List<string> delimiters = new List<string> { ",", "\\n" };

    if (Regex.IsMatch(input, SINGLE_CUSTOM_DELIMITER_REGEX))
    {
      char customDelimiter = input[DELIMITER_POSITION];
      delimiters.Add(customDelimiter.ToString());
      originalAddendString = input.Substring(ADDEND_STRING_START);
    }
    else if (Regex.IsMatch(input, MULTI_CUSTOM_DELIMITER_REGEX))
    {
      MatchCollection matches = Regex.Matches(input, @"\[(.*?)\]");
      foreach (Match match in matches)
      {
        delimiters.Add(match.Groups[1].Value);
      }
      originalAddendString = input.Substring(input.IndexOf("\\n") + 2);
    }
    else
    {
      originalAddendString = input;
    }

    string[] delimitersArray = delimiters.ToArray();

    return originalAddendString.Split(delimitersArray, StringSplitOptions.None).ToList();
  }

  private List<int> _Convert(List<string> addendStrings)
  {
    List<int> addends = new List<int>();
    foreach (string addendString in addendStrings)
    {
      try
      {
        int addend = int.Parse(addendString);
        addends.Add(addend);
      }
      catch (FormatException)
      {
        addends.Add(0); // treat non-numeric strings as 0
      }
    }
    return addends;
  }

  private void _AssertNoNegatives(List<int> addends)
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

  private List<int> _Transform(List<int> addends)
  {
    List<int> transformedAddends = new List<int>();
    foreach (int addend in addends)
    {
      if (addend > 1000)
      {
        transformedAddends.Add(0);
      }
      else
      {
        transformedAddends.Add(addend);
      }
    }
    return transformedAddends;
  }

  private int _Calculate(List<int> addends)
  {
    int sum = 0;
    foreach (int addend in addends)
    {
      sum += addend;
    }
    return sum;
  }
}
