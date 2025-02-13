using CalculatorApp.Converters;

namespace CalculatorApp.Tests;

public class StringToIntConverterTest
{
  [Fact]
  public void Converts_Numeric_Strings_To_Numbers()
  {
    List<string> inputs = new List<string> { "1", "2" };
    IStringToIntConverter stringToIntConverter = new StringToIntConverter();
    List<int> results = stringToIntConverter.Convert(inputs);
    Assert.Equal(results, new List<int> { 1, 2 });
  }

  [Fact]
  public void Converts_Non_Numeric_Strings_To_Zero()
  {
    List<string> inputs = new List<string> { "1", "abc", "2", "" };
    IStringToIntConverter stringToIntConverter = new StringToIntConverter();
    List<int> results = stringToIntConverter.Convert(inputs);
    Assert.Equal(results, new List<int> { 1, 0, 2, 0 });
  }
}