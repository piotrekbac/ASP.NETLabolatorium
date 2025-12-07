using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab0.Models
{
    [Table("publishers")]   // nadajemy odpowiednią nazwę tabeli w bazie danych
    public class Publisher
    {
        [Key]
        [Column("publisher_id")]
        public int PublisherId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("established_year")]
        public int? EstablishedYear { get; set; }
        
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}