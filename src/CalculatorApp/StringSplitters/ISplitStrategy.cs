namespace CalculatorApp.StringSplitters;

// each split strategy must provide a pattern to match an input string as well
// as a method to split the string into a list of strings. The StringSplitter
// will check each strategy in the order it was added to see if there is a match
// for the provided pattern. If so, the StringSplitter will run the Split method
// on the input string
public interface ISplitStrategy
{
  public string Pattern
  {
    get;
  }

  List<string> Split(string input);
}
