using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;

namespace CalculatorApp;

public class NegativeAddendException : Exception
{
  private List<int> _negativeAddends;

  public NegativeAddendException(List<int> negativeAddends) : base($"Negative addends provided: {string.Join(", ", negativeAddends)}")
  {
    _negativeAddends = negativeAddends;
  }
  public List<int> NegativeAddends => _negativeAddends;
}

public class Calculator
{
  private IStringSplitter _stringSplitter;
  private IOperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;
  private bool _shouldRejectNegatives;

  // a String Calculator will
  // - take a string as an input
  // - split the string into a list of strings
  // - convert the strings into integers
  // - transform the integers given certain transformations
  // - assert that no negative integers are present
  // - sum the integers
  public Calculator()
  {
    _stringSplitter = StringSplitter.Default;
    _operandTransformer = OperandTransformer.Default;
    _stringToIntConverter = new StringToIntConverter();
    _shouldRejectNegatives = true;
  }

  public void SetStringSplitter(IStringSplitter stringSplitter)
  {
    _stringSplitter = stringSplitter;
  }

  public void SetOperandTransformer(IOperandTransformer operandTransformers)
  {
    _operandTransformer = operandTransformers;
  }

  public void AllowNegatives()
  {
    _shouldRejectNegatives = false;
  }

  public int Calculate(string input)
  {
    List<string> addendStrings = _stringSplitter.Split(input);
    List<int> potentialAddends = _stringToIntConverter.Convert(addendStrings);
    if (_shouldRejectNegatives)
    {
      _AssertNoNegatives(potentialAddends);
    }
    List<int> addends = _operandTransformer.Transform(potentialAddends);
    int result = _Calculate(addends);
    return result;
  }

  public string DisplayFormula(string input)
  {
    // this duplicated code makes me sad but we will make it right later
    List<string> addendStrings = _stringSplitter.Split(input);
    List<int> potentialAddends = _stringToIntConverter.Convert(addendStrings);
    if (_shouldRejectNegatives)
    {
      _AssertNoNegatives(potentialAddends);
    }
    List<int> addends = _operandTransformer.Transform(potentialAddends);
    string formulaStart = string.Join("+", addends);
    int result = _Calculate(addends);
    string resultString = string.Join(" = ", new string[] { formulaStart, result.ToString() });
    return resultString;
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
