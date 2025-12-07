namespace Lab0.Models;

// Model używany do obsługi błędów w aplikacji
public class ErrorViewModel
{
    // Identyfikator żądania – pozwala łatwiej śledzić konkretne błędy
    public string? RequestId { get; set; }

    // Właściwość pomocnicza – zwraca true, jeśli RequestId nie jest pusty
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}