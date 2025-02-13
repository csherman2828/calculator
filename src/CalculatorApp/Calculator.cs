namespace CalculatorApp;

public class Calculator
{
  private IStringSplitter _stringSplitter;
  private IOperandTransformer _operandTransformer;
  private IStringToIntConverter _stringToIntConverter;
  private IOperandRules _operandRules;

  // a String Calculator will
  // - take a string as an input
  // - split the string into a list of strings
  // - convert the strings into integers
  // - transform the integers given certain transformations
  // - assert that no negative integers are present
  // - sum the integers
  public Calculator(
    IStringSplitter stringSplitter,
    IStringToIntConverter stringToIntConverter,
    IOperandRules operandRules,
    IOperandTransformer operandTransformer
  )
  {
    _stringSplitter = stringSplitter;
    _operandTransformer = operandTransformer;
    _stringToIntConverter = stringToIntConverter;
    _operandRules = operandRules;
  }

  public int Calculate(string input)
  {
    CalculatorResult calculatorResult = Solve(input);
    return calculatorResult.Answer;
  }

  public string DisplayFormula(string input)
  {
    CalculatorResult calculatorResult = Solve(input);
    return calculatorResult.Formula;
  }

  public CalculatorResult Solve(string input)
  {
    // this duplicated code makes me sad but we will make it right later
    List<string> addendStrings = _stringSplitter.Split(input);
    List<int> potentialAddends = _stringToIntConverter.Convert(addendStrings);
    _operandRules.Enforce(potentialAddends);
    List<int> addends = _operandTransformer.Transform(potentialAddends);
    string formulaStart = string.Join("+", addends);
    int answer = _Calculate(addends);
    string formula = string.Join(" = ", new string[] { formulaStart, answer.ToString() });
    return new CalculatorResult(answer, formula);
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
