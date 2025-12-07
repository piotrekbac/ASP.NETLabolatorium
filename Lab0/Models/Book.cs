using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models
{
    // Klasa Book odpowiada tabeli "books" w bazie danych
    [Table("books")]
    public class Book
    {
        // Id książki – klucz główny, ukryty w formularzach
        [HiddenInput]
        [Column("book_id")]
        public int Id { get; set; }

        // Tytuł książki – wymagany, długość od 2 do 100 znaków
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [Display(Name = "Tytuł")]
        [Column("title")]
        public string? Title { get; set; }

        // Autor – opcjonalny, ale jeśli podany to min. 2 znaki, max. 350
        [StringLength(maximumLength: 350, MinimumLength = 2)]
        [Display(Name = "Imię i nazwisko, bądź pseudonim autora")]
        [Column("author")]
        public string? Author { get; set; }

        // Liczba stron – musi mieścić się w zakresie od 1 do 10 000
        [Range(1, 10000)]
        [Column("page_number")]
        public int PageNumber { get; set; }

        // ISBN – wymagany, min. 5 znaków, max. 350
        [Required]
        [StringLength(maximumLength: 350, MinimumLength = 5)]
        [Column("isbn")]
        public string? ISBN { get; set; }

        // Rok wydania – dopuszczalny zakres od -350 (np. starożytne dzieła) do 3500
        [Range(-350, 3500)]
        [Column("year_of_publishing")]
        public int YearOfPublishing { get; set; }
        
        // Tekstowe pole wydawcy – np. nazwa wydawnictwa wpisana ręcznie
        [StringLength(maximumLength: 80, MinimumLength = 1)]
        [Column("publisher_text")]
        public string? Publisher { get; set; }
        

        // Klucz obcy (nullable): jeśli usuniemy wydawnictwo — książka pozostanie, a PublisherId zmieni się na null (zgodnie z Twoim wymaganiem)
        [Column("publisher_id")]
        public int? PublisherId { get; set; }

        // Nawigacja do encji Publisher – pozwala łatwo pobierać powiązane dane
        [ForeignKey("PublisherId")]
        public virtual Publisher? PublisherEntity { get; set; }
    }
}