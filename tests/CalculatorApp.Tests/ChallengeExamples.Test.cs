using CalculatorApp.Builder;
using CalculatorApp.OperandTransformers;
using CalculatorApp.StringSplitters;
using CalculatorApp.Converters;

namespace CalculatorApp.Tests;

public class ChallengeExampleTests
{
  private readonly Calculator _calculator;

  public ChallengeExampleTests()
  {
    CalculatorBuilder calculatorBuilder = new();

    StringSplitter stringSplitter = new();
    stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
    stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
    stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
    calculatorBuilder.SetStringSplitter(stringSplitter);

    StringToIntConverter stringToIntConverter = new();
    calculatorBuilder.SetStringToIntConverter(stringToIntConverter);

    OperandTransformer operandTransformer = new();
    operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
    calculatorBuilder.SetOperandTransformer(operandTransformer);

    _calculator = calculatorBuilder.Build();
  }

  [Fact]
  public void Req_1_20()
  {

    int result = _calculator.Calculate("20");
    Assert.Equal(20, result);
  }

  [Fact]
  public void Req_1_1_5000()
  {

    int result = _calculator.Calculate("1,5000");

    // would have previously been different
    // Assert.Equal(5001, result);

    Assert.Equal(1, result);
  }

  [Fact]
  public void Req_1_5_tytyt()
  {

    int result = _calculator.Calculate("5,tytyt");

    // would have previously been different
    // Assert.Equal(5, result);

    Assert.Equal(5, result);
  }

  [Fact]
  public void Req_2_1_to_12()
  {

    int result = _calculator.Calculate("1,2,3,4,5,6,7,8,9,10,11,12");
    Assert.Equal(78, result);
  }

  [Fact]
  public void Req_3_1_Newline_2_Comma_3()
  {

    int result = _calculator.Calculate("1\\n2,3");
    Assert.Equal(6, result);
  }

  [Fact]
  public void Req_5_2_1001_6()
  {

    int result = _calculator.Calculate("2,1001,6");
    Assert.Equal(8, result);
  }

  [Fact]
  public void Req_6_Custom_Delimiter_Pound()
  {
    int result = _calculator.Calculate("//#\\n2#5");
    Assert.Equal(7, result);
  }

  [Fact]
  public void Req_6_Comma()
  {
    int result = _calculator.Calculate("//,\\n2,ff,100");
    Assert.Equal(102, result);
  }

  [Fact]
  public void Req_7_Triple_Asterisk()
  {
    int result = _calculator.Calculate("//[***]\\n11***22***33");
    Assert.Equal(66, result);
  }

  [Fact]
  public void Req_8_Multiple_Custom_Delimiters()
  {
    int result = _calculator.Calculate("//[*][!!][r9r]\\n11r9r22*hh*33!!44");
    Assert.Equal(110, result);
  }

  [Fact]
  public void Stretch_1_Formula()
  {

    string result = _calculator.DisplayFormula("2,,4,rrrr,1001,6");
    Assert.Equal("2+0+4+0+0+6 = 12", result);
  }
}