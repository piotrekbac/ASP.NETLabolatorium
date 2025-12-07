using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models
{
    [Table("books")]
    public class Book
    {
        [HiddenInput]
        [Column("book_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [Display(Name = "Tytuł")]
        [Column("title")]
        public string? Title { get; set; }

        [StringLength(maximumLength: 350, MinimumLength = 2)]
        [Display(Name = "Imię i nazwisko, bądź pseudonim autora")]
        [Column("author")]
        public string? Author { get; set; }

        [Range(1, 10000)]
        [Column("page_number")]
        public int PageNumber { get; set; }

        [Required]
        [StringLength(maximumLength: 350, MinimumLength = 5)]
        [Column("isbn")]
        public string? ISBN { get; set; }

        [Range(-350, 3500)]
        [Column("year_of_publishing")]
        public int YearOfPublishing { get; set; }
        
        [StringLength(maximumLength: 80, MinimumLength = 1)]
        [Column("publisher_text")]
        public string? Publisher { get; set; }
        

        // Klucz obcy (nullable): jeśli usuniemy wydawnictwo — książka pozostanie, a PublisherId zmieni się na null (zgodnie z Twoim wymaganiem)
        [Column("publisher_id")]
        public int? PublisherId { get; set; }

        // Nawigacja do Publisher
        [ForeignKey("PublisherId")]
        public virtual Publisher? PublisherEntity { get; set; }
    }
}