using CalculatorApp.StringSplitters;

namespace CalculatorApp.Tests;

public class StringSplitterTest
{
  [Fact]
  public void Default_Splits_Single_Number()
  {
    StringSplitter stringSplitter = StringSplitter.Default;
    string input = "20";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "20" }, result);
  }

  [Fact]
  public void Default_Splits_Comma_Delimited_Numbers()
  {
    StringSplitter stringSplitter = StringSplitter.Default;
    string input = "1,2,3";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "1", "2", "3" }, result);
  }

  [Fact]
  public void Default_Splits_Newline_Delimited_Numbers()
  {
    StringSplitter stringSplitter = StringSplitter.Default;
    string input = "1\\n2\\n3";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "1", "2", "3" }, result);
  }

  [Fact]
  public void Default_Splits_Wacky_Input()
  {
    StringSplitter stringSplitter = StringSplitter.Default;
    string input = ",,,\\n1,,,,,2\\n\\n\\n3,";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "", "", "", "", "1", "", "", "", "", "2", "", "", "3", "", }, result);
  }

  [Fact]
  public void CustomSingleCharSplitStrategy_Semicolon()
  {
    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    string input = "//;\\n1;2;3";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "1", "2", "3" }, result);
  }

  [Fact]
  public void CustomMultiStringSplitStrategy_Asterisks()
  {
    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    string input = "//[***]\\n1***2***3";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "1", "2", "3" }, result);
  }

  [Fact]
  public void Use_Custom_Alternative_Delimiter()
  {
    string alternativeDelimiter = "##";
    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy(alternativeDelimiter));
    string input = "1##2,3\\n4##5";
    List<string> result = stringSplitter.Split(input);
    Assert.Equal(new List<string> { "1", "2", "3\\n4", "5" }, result);
  }
}