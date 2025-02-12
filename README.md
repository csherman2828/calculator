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

You can add numbers together by presenting them as a comma separated list.

```
// 2 + 3 = 5
add> 2,3
5

// 2 = 2
add> 2
```

You can include negative numbers in your calculation (which can simulate subtraction).

```
// 2 - 5 + 10 = 7
// 2 + (-5) + 10 = 7
add> 2,-5,10
7
```

The program ends after one addition operation.
