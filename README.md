# CalculatorApp

A CLI based calculator built in C# on .NET Core.

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

You can add up to two numbers together by presenting them as a comma separated
list.

```
// 2 + 3 = 5
add> 2,3
5

// 2 = 2
add> 2
```

The program ends after one addition operation.

If you try to add more than three numbers, the program crashes.

```
// 1 + 2 + 3 = 6
add> 1,2,3

Unhandled exception. CalculatorApp.Utils.TooManyAddendsException: More than two addends provided
   at CalculatorApp.Utils.Calculator.Calculate(String input) in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Calculator.cs:line 16
   at CalculatorApp.Program.Main() in /home/csherman2828/projects/calculator-csharp/Calculator/src/CalculatorApp/Program.cs:line 16
```
