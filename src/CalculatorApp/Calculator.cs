using CalculatorApp.StringSplitters;
using CalculatorApp.OperandTransformers;
using CalculatorApp.Converters;

namespace CalculatorApp.Utils;

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
  private StringSplitter _stringSplitter;
  private OperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;
  private bool _shouldRejectNegatives;

  public Calculator()
  {
    _stringSplitter = StringSplitter.Default;
    _operandTransformer = OperandTransformer.Default;
    _stringToIntConverter = new StringToIntConverter();
    _shouldRejectNegatives = true;
  }

  public void SetStringSplitter(StringSplitter stringSplitter)
  {
    _stringSplitter = stringSplitter;
  }

  public void SetOperandTransformer(OperandTransformer operandTransformers)
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
    List<int> potentialAddends = _Convert(addendStrings);
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
