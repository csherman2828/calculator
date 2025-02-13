namespace CalculatorApp.StringSplitters;

// this split strategy looks for a custom delimiter at the beginning of the
// string. To be applied, the input must match the pattern:
//
// //{delimiterChar}\n{addendString}
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
