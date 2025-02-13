
namespace CalculatorApp;
public class CalculatorArgs
{
  private bool _shouldAllowNegatives;
  private int _upperBound;

  public CalculatorArgs(string[] args)
  {
    _shouldAllowNegatives = false;
    _upperBound = 1000;

    for (int argIdx = 0; argIdx < args.Length; argIdx++)
    {
      string arg = args[argIdx];
      if (arg == "-n")
      {
        _shouldAllowNegatives = true;
      }

      if (arg == "-u")
      {
        if (argIdx + 1 < args.Length)
        {
          if (int.TryParse(args[argIdx + 1], out int upperBound))
          {
            _upperBound = upperBound;
          }
          argIdx++;
        }
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

  public int UpperBound
  {
    get
    {
      return _upperBound;
    }
  }
}