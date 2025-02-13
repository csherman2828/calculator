using CalculatorApp.Utils;
using CalculatorApp.StringSplitters;

namespace MyApp.Tests;

public class ChallengeExampleTests
{
  [Fact]
  public void Req_1_20()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("20");
    Assert.Equal(20, result);
  }

  [Fact]
  public void Req_1_1_5000()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("1,5000");

    // would have previously been different
    // Assert.Equal(5001, result);

    Assert.Equal(1, result);
  }

  [Fact]
  public void Req_1_5_tytyt()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("5,tytyt");

    // would have previously been different
    // Assert.Equal(5, result);

    Assert.Equal(5, result);
  }

  [Fact]
  public void Req_2_1_to_12()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("1,2,3,4,5,6,7,8,9,10,11,12");
    Assert.Equal(78, result);
  }

  [Fact]
  public void Req_3_1_Newline_2_Comma_3()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("1\\n2,3");
    Assert.Equal(6, result);
  }

  [Fact]
  public void Req_5_2_1001_6()
  {
    Calculator calculator = new();
    int result = calculator.Calculate("2,1001,6");
    Assert.Equal(8, result);
  }

  [Fact]
  public void Req_6_Custom_Delimiter_Pound()
  {
    Calculator calculator = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    calculator.SetStringSplitter(stringSplitter);

    int result = calculator.Calculate("//#\\n2#5");
    Assert.Equal(7, result);
  }

  [Fact]
  public void Req_6_Comma()
  {
    Calculator calculator = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    calculator.SetStringSplitter(stringSplitter);

    int result = calculator.Calculate("//,\\n2,ff,100");
    Assert.Equal(102, result);
  }

  [Fact]
  public void Req_7_Triple_Asterisk()
  {
    Calculator calculator = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    calculator.SetStringSplitter(stringSplitter);

    int result = calculator.Calculate("//[***]\\n11***22***33");
    Assert.Equal(66, result);
  }

  [Fact]
  public void Req_8_Multiple_Custom_Delimiters()
  {
    Calculator calculator = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    calculator.SetStringSplitter(stringSplitter);

    int result = calculator.Calculate("//[*][!!][r9r]\\n11r9r22*hh*33!!44");
    Assert.Equal(110, result);
  }

  [Fact]
  public void Stretch_1_Formula()
  {
    Calculator calculator = new();
    string result = calculator.DisplayFormula("2,,4,rrrr,1001,6");
    Assert.Equal("2+0+4+0+0+6 = 12", result);
  }
}