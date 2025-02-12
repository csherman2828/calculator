﻿using CalculatorApp.Utils;

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
    public void Calculates_Given_Three_Addends()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,2,3");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculates_Given_Twelve_Addends()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,2,3,4,5,6,7,8,9,10,11,12");
        Assert.Equal(78, result);
    }

    [Fact]
    public void Calculates_Given_One_Thousand_Addends()
    {
        Calculator calculator = new();
        int[] addends = new int[1000];
        for (int i = 0; i < 1000; i++)
        {
            addends[i] = 1;
        }
        string input = string.Join(",", addends);
        int result = calculator.Calculate(input);
        Assert.Equal(1000, result);
    }

    [Fact]
    public void Treats_Empty_String_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Comma_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate(",");
        Assert.Equal(0, result);
    }

    [Fact]
    public void Treats_Trailing_Comma_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,");
        Assert.Equal(1, result);
    }

    [Fact]
    public void Treats_Leading_Comma_As_Zero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate(",1");
        Assert.Equal(1, result);
    }

    [Fact]
    public void TreatsAllEmptyStringsAsZero()
    {
        Calculator calculator = new();
        int result = calculator.Calculate(",,,2,,,,,3,,,5,,");
        Assert.Equal(10, result);
    }

    [Fact]
    public void Throws_Exception_Listing_Negative_Numbers()
    {
        Calculator calculator = new();
        NegativeAddendException e = Assert.Throws<NegativeAddendException>(delegate { calculator.Calculate("-1,2,-3"); });
        Assert.Equal(new List<int> { -1, -3 }, e.GetNegativeAddends);
        Assert.Equal("Negative addends provided: -1, -3", e.Message);
    }

    [Fact]
    public void Splits_Addends_On_Newline_And_Comma()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1\n2,3\n4\n5");
        Assert.Equal(15, result);
    }

    [Fact]
    public void Ignores_Addends_Over_One_Thousand()
    {
        Calculator calculator = new();
        int result = calculator.Calculate("1,2,3,4,5,1001");
        Assert.Equal(15, result);
    }
}