using Calculator.Enums;

namespace Calculator;

public class Calculator
{
    private const string _addSymbol = "+";
    private const string _subtractSymbol = "-";
    private const string _multiplySymbol = "*";
    private const string _divideSymbol = "/";

    public bool TryParseInput(
        string[] arr,
        out int firstOperand,
        out MathAction action,
        out int secondOperand )
    {
        firstOperand = 0;
        action = Enums.MathAction.InvalidAction;
        secondOperand = 0;

        if ( arr.Length != 3 )
        {
            return false;
        }

        if ( !int.TryParse( arr[ 0 ], out firstOperand ) )
        {
            return false;
        }

        if ( !TryParseAction( arr[ 1 ], out action ) )
        {
            return false;
        }

        if ( !int.TryParse( arr[ 2 ], out secondOperand ) )
        {
            return false;
        }

        if ( secondOperand == 0 && action == MathAction.Divide )
        {
            return false;
        }

        return true;
    }

    public bool TryParseAction( string input, out MathAction action )
    {
        switch ( input )
        {
            case _addSymbol:
                action = MathAction.Add;
                return true;
            case _subtractSymbol:
                action = MathAction.Subtract;
                return true;
            case _multiplySymbol:
                action = MathAction.Multiply;
                return true;
            case _divideSymbol:
                action = MathAction.Divide;
                return true;
            default:
                action = MathAction.InvalidAction;
                return false;
        }
    }

    public bool IsOverflow( int firstOperand, int secondOperand )
    {
        try
        {
            checked
            {
                int add = firstOperand + secondOperand;
                int sub = firstOperand - secondOperand;
                int mul = firstOperand * secondOperand;
            }
        }
        catch ( OverflowException )
        {
            return true;
        }
        return false;
    }

    public bool TryHandleAction( int firstOperand, MathAction action, int secondOperand, out int result )
    {
        result = 0;
        try
        {
            checked
            {
                switch ( action )
                {
                    case MathAction.Add:
                        result = firstOperand + secondOperand;
                        return true;
                    case MathAction.Subtract:
                        result = firstOperand - secondOperand;
                        return true;
                    case MathAction.Multiply:
                        result = firstOperand * secondOperand;
                        return true;
                    case MathAction.Divide:
                        result = firstOperand / secondOperand;
                        return true;
                    default:
                        result = 0;
                        return false;
                }
            }
        }
        catch ( OverflowException )
        {
            return false;
        }
    }
}
