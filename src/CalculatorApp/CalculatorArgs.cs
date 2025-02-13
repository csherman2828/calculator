
namespace CalculatorApp;
public class CalculatorArgs
{
  private bool _shouldAllowNegatives;
  private int _upperBound;
  private string _alternativeDefaultDelim;
  private string _operation;

  public CalculatorArgs(string[] args)
  {
    _shouldAllowNegatives = false;
    _upperBound = 1000;
    _alternativeDefaultDelim = "\\n";
    _operation = "ADD";

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

      if (arg == "-d")
      {
        if (argIdx + 1 < args.Length)
        {
          _alternativeDefaultDelim = args[argIdx + 1];
          argIdx++;
        }
      }

      if (arg == "-o")
      {
        if (argIdx + 1 < args.Length)
        {
          _operation = args[argIdx + 1].ToUpper();
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

  public string AlternativeDefaultDelim
  {
    get
    {
      return _alternativeDefaultDelim;
    }
  }

  public string Operation
  {
    get
    {
      return _operation;
    }
  }
}