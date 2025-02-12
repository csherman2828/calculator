namespace CalculatorApp.Utils;

public class TooManyAddendsException : Exception
{
  public TooManyAddendsException() : base("More than two addends provided") { }
}

public class Calculator
{
  public int Calculate(string input)
  {
    string[] numbersToAdd = input.Split(",");

    if (numbersToAdd.Length > 2)
    {
      throw new TooManyAddendsException();
    }

    int sum = 0;
    foreach (var number in numbersToAdd)
    {
      try
      {
        int parsedInt = int.Parse(number);
        sum += parsedInt;
      }
      catch (FormatException) { }
    }
    return sum;
  }
}
