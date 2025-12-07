namespace Lab0.Models;

public class Organization
{
    // Klucz główny – unikalny identyfikator organizacji
    public int Id { get; set; }
    
    // Nazwa organizacji – np. "WSEI"
    public string Name { get; set; }
    
    // Opis organizacji – dodatkowe informacje, np. "Uczelnia"
    public string Description { get; set; }
}