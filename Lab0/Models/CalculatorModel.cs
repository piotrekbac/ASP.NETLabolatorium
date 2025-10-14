namespace Lab0.Models;

public class CalculatorModel
{
    public double? A {get; set;}
    public double? B {get; set;}
    public Operators Op {get; set;}

    public bool IsValid()
    {
        return A is not null && B is not null &&  Op != Operators.Unknown;
    }
    
    public string Result()
    {
        string result = "";
        switch (Op)
        {
            case Operators.Add: result = $"{A} + {B} = {A + B}";
                break;
            case Operators.Sub: result = $"{A} - {B} = {A - B}";
                break;
            case Operators.Mul: result = $"{A} * {B} = {A * B}";
                break;
            case Operators.Div: result = $"{A} / {B} = {A / B}";
                break;
            default:
                result = "Nieznany operator!";
                break;
        }
        return result;
    }
}