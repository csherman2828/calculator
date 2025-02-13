using CalculatorApp.OperandTransformers;

namespace CalculatorApp.Tests;

public class OperandTransformerTest
{
  [Fact]
  public void Test_Transform()
  {
    List<int> operands = new() { 1, 2, 3, 4, 5 };
    OperandTransformer transformer = new();
    transformer.AddTransformation(new UpperBoundTransformation(1000));
    List<int> transformedOperands = transformer.Transform(operands);
    Assert.Equal(operands, transformedOperands);
  }

  [Fact]
  public void Test_Transform_With_UpperBound()
  {
    List<int> operands = new() { 1, 2, 3, 4, 5, 1001 };
    OperandTransformer transformer = new();
    transformer.AddTransformation(new UpperBoundTransformation(1000));
    List<int> transformedOperands = transformer.Transform(operands);
    Assert.Equal(new List<int> { 1, 2, 3, 4, 5, 0 }, transformedOperands);
  }
}