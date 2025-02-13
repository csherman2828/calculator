using System.Text.RegularExpressions;

namespace CalculatorApp.StringSplitters;

public class CustomMultiStringSplitStrategy : ISplitStrategy
{
  public string Pattern => @"^//(\[[^\]]*\])+\\n.*";

  public List<string> Split(string input)
  {
    List<string> delimiters = new();
    MatchCollection matches = Regex.Matches(input, @"\[(.*?)\]");
    foreach (Match match in matches)
    {
      delimiters.Add(match.Groups[1].Value);
    }
    string originalAddendString = input.Substring(input.IndexOf("\\n") + 2);
    string[] delimitersArray = delimiters.ToArray();
    return originalAddendString.Split(delimitersArray, StringSplitOptions.None).ToList();
  }
}
