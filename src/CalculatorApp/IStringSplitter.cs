namespace CalculatorApp;

// a string splitter has a split method that takes a string and returns a list
// of strings, especially separated tokens from the original string
public interface IStringSplitter
{
  public List<string> Split(string input);
}
