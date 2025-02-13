namespace CalculatorApp;

public class Calculator
{
  private ICalculatorOperation _operation;
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
    ICalculatorOperation operation,
    IStringSplitter stringSplitter,
    IStringToIntConverter stringToIntConverter,
    IOperandRules operandRules,
    IOperandTransformer operandTransformer
  )
  {
    _operation = operation;
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
    // we will rely on injected dependencies to do the non-calculating work
    List<string> addendStrings = _stringSplitter.Split(input);
    List<int> potentialAddends = _stringToIntConverter.Convert(addendStrings);
    _operandRules.Enforce(potentialAddends);
    List<int> addends = _operandTransformer.Transform(potentialAddends);
    string formulaStart = string.Join("+", addends);
    int answer = _Calculate(addends);
    string formula = string.Join(" = ", new string[] { formulaStart, answer.ToString() });
    return new CalculatorResult(answer, formula);
  }

  private int _Calculate(List<int> operands)
  {
    if (operands.Count == 0)
    {
      return _operation.Identity;
    }

    int firstOperand = operands[0];
    int result = firstOperand;
    for (int operandIdx = 1; operandIdx < operands.Count; operandIdx++)
    {
      int operand = operands[operandIdx];
      result = _operation.Operate(result, operand);
    }
    return result;
  }
}
