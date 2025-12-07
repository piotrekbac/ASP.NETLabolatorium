namespace Lab0.Models;

// Model kalkulatora – przechowuje dwie liczby i operator
public class CalculatorModel
{
    // pierwsza liczba (może być null, jeśli nie podano)
    public double? A {get; set;}
    
    // druga liczba (może być null, jeśli nie podano)
    public double? B {get; set;}
    
    // operator matematyczny (dodawanie, odejmowanie, itd.)
    public Operators Op {get; set;}

    // sprawdzamy czy dane są poprawne: obie liczby podane i operator nie jest "Unknown"
    public bool IsValid()
    {
        return A is not null && B is not null &&  Op != Operators.Unknown;
    }
    
    // metoda zwraca wynik w formie tekstu, np. "2 + 3 = 5"
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
                
                // jeśli operator nieznany – komunikat błędu
                result = "Nieznany operator!";
                break;
        }
        return result;
    }
}