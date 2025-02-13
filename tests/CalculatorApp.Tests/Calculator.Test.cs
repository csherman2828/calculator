using CalculatorApp.Builder;
using CalculatorApp.OperandTransformers;
using CalculatorApp.StringSplitters;
using CalculatorApp.Converters;
using CalculatorApp.Rules;
using CalculatorApp;

namespace MyApp.Tests;

public class CalculatorApp_Utils_Calculator
{
    private readonly Calculator _calculator;

    public CalculatorApp_Utils_Calculator()
    {
        CalculatorBuilder calculatorBuilder = new();

        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
        calculatorBuilder.SetStringSplitter(stringSplitter);

        StringToIntConverter stringToIntConverter = new();
        calculatorBuilder.SetStringToIntConverter(stringToIntConverter);

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());
        calculatorBuilder.SetOperandRules(operandRules);

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
        calculatorBuilder.SetOperandTransformer(operandTransformer);

        _calculator = calculatorBuilder.Build();
    }

    [Fact]
    public void Calculates_One_Addend()
    {

        int result = _calculator.Calculate("1");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Calculates_Two_Addends()
    {

        int result = _calculator.Calculate("1,2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Calculates_Given_Three_Addends()
    {

        int result = _calculator.Calculate("1,2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculates_Given_One_Thousand_Addends()
    {

        int[] addends = new int[1000];
        for (int i = 0; i < 1000; i++)
        {
            addends[i] = 1;
        }
        string input = string.Join(",", addends);
        int result = _calculator.Calculate(input);
        Assert.Equal(1000, result);
    }

    [Fact]
    public void Treats_Empty_String_As_Zero()
    {

        int result = _calculator.Calculate("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Comma_As_Zero()
    {

        int result = _calculator.Calculate(",");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Trailing_Comma_As_Zero()
    {

        int result = _calculator.Calculate("1,");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Treats_Leading_Comma_As_Zero()
    {

        int result = _calculator.Calculate(",1");
        Assert.Equal(1, result);
    }

    [Fact]
    public void TreatsAllEmptyStringsAsZero()
    {

        int result = _calculator.Calculate(",,,2,,,,,3,,,5,,");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Throws_Exception_Listing_Negative_Numbers()
    {

        NegativeOperandException e = Assert.Throws<NegativeOperandException>(delegate { _calculator.Calculate("-1,2,-3"); });
        Assert.Equal(new List<int> { -1, -3 }, e.NegativeAddends);
        Assert.Equal("Negative addends provided: -1, -3", e.Message);
    }

    [Fact]
    public void Splits_Addends_On_Newline_And_Comma()
    {

        // escaped backslash newlines is how the standard input "\n" gets parsed
        int result = _calculator.Calculate("1\\n2,3\\n4\\n\\n5,6,,,,\n\n");
        Assert.Equal(21, result);
    }

    [Fact]
    public void Ignores_Addends_Over_One_Thousand()
    {

        int result = _calculator.Calculate("1,2,3,4,5,1001");
        Assert.Equal(15, result);
    }

    [Fact]
    public void Toggle_Negative_Denial_Off()
    {
        CalculatorBuilder calculatorBuilder = new();

        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
        calculatorBuilder.SetStringSplitter(stringSplitter);

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
        calculatorBuilder.SetOperandTransformer(operandTransformer);

        OperandRules operandRules = new();
        calculatorBuilder.SetOperandRules(operandRules);

        Calculator calculator = calculatorBuilder.Build();

        int result = calculator.Calculate("1,-2,3");
        Assert.Equal(2, result);
    }

    [Fact]
    public void Handle_Custom_Upper_Bound()
    {
        int upperBound = 100;

        CalculatorBuilder calculatorBuilder = new();

        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());
        calculatorBuilder.SetStringSplitter(stringSplitter);

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(upperBound));
        calculatorBuilder.SetOperandTransformer(operandTransformer);

        OperandRules operandRules = new();
        calculatorBuilder.SetOperandRules(operandRules);

        Calculator calculator = calculatorBuilder.Build();

        int result = calculator.Calculate("1,102,-1");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Handle_Custom_Alternative_Delimiter()
    {
        string alternativeDelimiter = "##";

        CalculatorBuilder calculatorBuilder = new();

        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy(alternativeDelimiter));
        calculatorBuilder.SetStringSplitter(stringSplitter);

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
        calculatorBuilder.SetOperandTransformer(operandTransformer);

        OperandRules operandRules = new();
        calculatorBuilder.SetOperandRules(operandRules);

        Calculator calculator = calculatorBuilder.Build();

        int result = calculator.Calculate("1##2,3\\n4##3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Handles_Explicit_Add()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("ADD")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        int result = calculator.Calculate("1,2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Handles_Explicit_Subtract()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("SUBTRACT")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        int result = calculator.Calculate("5,2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Handles_Explicit_Multiply()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("MULTIPLY")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        int result = calculator.Calculate("2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Handles_Explicit_Divide()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("DIVIDE")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        int result = calculator.Calculate("6,2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Shows_Correct_Formula_For_Subtraction()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("SUBTRACT")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        string result = calculator.DisplayFormula("7,2,3");
        Assert.Equal("7-2-3 = 2", result);
    }

    [Fact]
    public void Shows_Correct_Formula_For_Addition()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("ADD")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        string result = calculator.DisplayFormula("1,2,3");
        Assert.Equal("1+2+3 = 6", result);
    }

    [Fact]
    public void Shows_Correct_Formula_For_Multiplication()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("MULTIPLY")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        string result = calculator.DisplayFormula("2,3,4");
        Assert.Equal("2*3*4 = 24", result);
    }

    [Fact]
    public void Shows_Correct_Formula_For_Division()
    {
        StringSplitter stringSplitter = new();
        stringSplitter.AddSplitStrategy(new CustomSingleCharSplitStrategy());
        stringSplitter.AddSplitStrategy(new CustomMultiStringSplitStrategy());
        stringSplitter.AddSplitStrategy(new DefaultSplitStrategy());

        OperandRules operandRules = new();
        operandRules.AddRule(new NoNegativesRule());

        OperandTransformer operandTransformer = new();
        operandTransformer.AddTransformation(new UpperBoundTransformation(1000));

        CalculatorBuilder calculatorBuilder = new();
        Calculator calculator = calculatorBuilder
          .SetOperation("DIVIDE")
          .SetStringSplitter(stringSplitter)
          .SetStringToIntConverter(new StringToIntConverter())
          .SetOperandRules(operandRules)
          .SetOperandTransformer(operandTransformer)
          .Build();

        string result = calculator.DisplayFormula("8,2,2");
        Assert.Equal("8/2/2 = 2", result);
    }
}