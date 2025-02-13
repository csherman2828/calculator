namespace CalculatorApp.StringSplitters;

public class CustomSingleCharSplitStrategy : ISplitStrategy
{
  private const int DELIMITER_INDEX = 2;
  private const int ADDEND_STRING_START_INDEX = 5;

  public string Pattern => @"^//.\\n.*";

  public List<string> Split(string input)
  {
    char customDelimiter = input[DELIMITER_INDEX];
    string originalAddendString = input.Substring(ADDEND_STRING_START_INDEX);
    return originalAddendString.Split(customDelimiter).ToList();
  }
}
