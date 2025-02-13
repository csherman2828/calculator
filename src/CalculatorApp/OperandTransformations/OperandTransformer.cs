namespace CalculatorApp.OperandTransformers;
public class OperandTransformer
{
  private List<IOperandTransformation> _transformations;

  public OperandTransformer()
  {
    _transformations = new List<IOperandTransformation>();
  }

  public void AddTransformation(IOperandTransformation transformation)
  {
    _transformations.Add(transformation);
  }

  public List<int> Transform(List<int> operands)
  {
    foreach (var transformation in _transformations)
    {
      for (int i = 0; i < operands.Count; i++)
      {
        operands[i] = transformation.Transform(operands[i]);
      }
    }
    return operands;
  }

  public static OperandTransformer Default
  {
    get
    {
      OperandTransformer operandTransformer = new OperandTransformer();
      operandTransformer.AddTransformation(new UpperBoundTransformation(1000));
      return operandTransformer;
    }
  }
}