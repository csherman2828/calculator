namespace CalculatorApp.OperandTransformers;

// a transformation takes one int and may or may not turn it into another int
public interface IOperandTransformation
{
  int Transform(int input);
}
