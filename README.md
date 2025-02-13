# String Calculator

A CLI based calculator built in C# on .NET Core. You can provide specially formatted strings to execute arithmetic operations. More details on use are detailed below.

## Build

```
dotnet build
```

## Run Tests

```
dotnet test
```

## Run CalculatorApp

```
dotnet run --project src/CalculatorApp
```

## How to Use

After you start the program, you will be prompted to add numbers together:

```
CalculatorApp
Type in 1 or 2 numbers separated by a comma to calculate their sum

Examples:
add> 1
add> 1,2

------------------------

add> 
```

You can add numbers together by presenting them as a comma separated list. In return, you
will get the formula associated with the string you provided, as well as the result of the
arithmetic operations.

```
// 2 + 3 = 5
add> 2,3
2+3 = 5

// 2 = 2
add> 2
```

You can provide any number of inputs.

```
add> 1,2,3,4,5,6,7,8,9
1+2+3+4+5+6+7+8+9 = 45
```

When you are done, you can input `Ctrl + C` and the program will stop, bringing you back
to your command line interface (e.g. bash, zsh, etc.)

### Negative Numbers Are Denied as Input By Default

If you include a negative number in your input, the program will crash.

```
add> 1,-2,3
Unhandled exception. CalculatorApp.Rules.NegativeOperandException: Negative addends provided: -2
   at CalculatorApp.Rules.NoNegativesRule.Enforce(List`1 operands) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Rules/NoNegativesRule.cs:line 10
   at CalculatorApp.Rules.OperandRules.Enforce(List`1 operands) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Rules/OperandRules.cs:line 21
   at CalculatorApp.Calculator.Solve(String input) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Calculator.cs:line 50
   at CalculatorApp.Calculator.DisplayFormula(String input) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Calculator.cs:line 41
   at CalculatorApp.Program.Main(String[] args) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Program.cs:line 43
```

#### Enabling Negative Numbers as Input

You can force the program to accept negative numbers by providing the "-n" option
when starting the program.

```
dotnet run --project src/CalculatorApp -n
...
add> 1,-2,3
1+-2+3 = 4
```

### Alternate Delimiter

We provide one more delimiter other than a comma by default. You can enter the "newline" character (\n)
as the delimiter between your strings.

```
add> 1\n2
1+2 = 3
```

#### Customizing Your Alternate Delimiter

You can customize this by providing options when opening the program. Include the `-d` option and provide your
custom alternate delimiter directly after.

```
dotnet run --project src/CalculatorApp -d *
...
add> 1,2*3
1+2+3 = 6
```

### By Default, Upper Bound is 1000

If any of the operands you provide are greater than 1000, they will be changed to 0. 

```
add> 1,1001,2
1+0+2 = 3
```

#### Customizing Your Upper Bound

You can adjust this upper bound with the `-u` option on the command line when running the program.

```
dotnet run --project src/CalculatorApp -u 100
...
add> 1,101,2
1+0+2 = 3
```

### Custom Delimiter Syntax

You can use a special syntax to describe how you want your string to be split up into operators.

#### Single Character Custom Delimiter

To use one specific character to delimit a string of integers, you can apply the following
format to your input.

```
//{delimiter}\n{numbers}
```

For example...

```
add> //;\n1;2;3
1+2+3 = 6
```

#### Multiple String Custom Delimiter

Using the following format, you can specify a set of delimiters made up of one or more characters in a string.

```
//[{delimiter1}][{delimiter2}]...\n{numbers}
```

For example, if your numbers are separated sometimes by `&&&` and sometimes by `##`, you could process a string as follows:

```
add> //[&&&][##]\n1&&&2##3
1+2+3 = 6
```

### Add, Subtract, Multiply, and Divide

By default, this program will add the numbers that are delimited in your strings together. However,
you can use the `-o` option to substitute your own operation.

#### Subtract

```
dotnet run --project src/CalculatorApp -o subtract
...
subtract> 6,2
6-2 = 4;
```

#### Multiply

```
dotnet run --project src/CalculatorApp -o multiply
...
multiply> 2,3
2*3 = 6;
```

#### Divide

```
dotnet run --project src/CalculatorApp -o divide
...
divide> 24,3
24/3 = 8
```
