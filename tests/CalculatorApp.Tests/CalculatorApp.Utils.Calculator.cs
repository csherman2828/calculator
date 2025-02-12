using CalculatorApp.Utils;

namespace MyApp.Tests;

public class CalculatorApp_Utils_Calculator
{
    [Fact]
    public void Calculates_Two_Addends()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,2");
        Assert.Equal(3, result);
    }

    [Fact]
    public void Calculates_One_Addend()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Throws_TooManyAddendsException_Given_Three_Addends()
    {
        Calculator calculator = new();
        Assert.Throws<TooManyAddendsException>(() => calculator.Calculate("1,2,3"));
    }

    [Fact]
    public void Throws_TooManyAddendsException_Given_Five_Addends()
    {
        Calculator calculator = new();
        Assert.Throws<TooManyAddendsException>(() => calculator.Calculate("1,2,3,4,5"));
    }

    [Fact]
    public void Returns_Zero_Given_Empty_String()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Empty_String_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Treats_Comma_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate(",");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Leading_Comma_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate(",1");
        Assert.Equal(1, result);
    }
}