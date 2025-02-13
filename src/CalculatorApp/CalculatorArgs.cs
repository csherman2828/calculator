
namespace CalculatorApp;
public class CalculatorArgs
{
  private bool _shouldAllowNegatives;
  public CalculatorArgs(string[] args)
  {
    _shouldAllowNegatives = false;

    foreach (string arg in args)
    {
      if (arg == "-n")
      {
        _shouldAllowNegatives = true;
      }
    }
  }

  public bool ShouldAllowNegatives
  {
    get
    {
      return _shouldAllowNegatives;
    }
  }
}