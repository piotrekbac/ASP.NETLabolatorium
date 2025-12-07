using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

// DbContext rozszerzony o Identity – dzięki temu mamy obsługę użytkowników i ról
public class AddDbContext : IdentityDbContext<IdentityUser>
{
    // konstruktor – przekazujemy opcje konfiguracji do bazy
    public AddDbContext(DbContextOptions<AddDbContext> options) : base(options)
    {
    }

    // DbSety – nasze tabele w bazie
    public DbSet<Book> Books { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Organization> Organization { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Wywołuję metodę bazową, która jest niezbędna do konfiguracji Identity
        base.OnModelCreating(modelBuilder);

        // Konfiguruję relację jeden do wielu (Jeden wydawca -> Wiele książek)
        // stosuję tutaj również OnDelete(setNull), aby po usunięciu wydawcy, nie usuwało powiązanych książek
        modelBuilder.Entity<Book>()
            .HasOne(b => b.PublisherEntity)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.SetNull);

        // tutaj odbywa się inicjalizacja danych poczatkowych do bazy
        modelBuilder.Entity<Publisher>().HasData(
            new Publisher() { PublisherId = 1, Name = "Greg", EstablishedYear = 1990 },
            new Publisher() { PublisherId = 2, Name = "Helion", EstablishedYear = 1991 },
            new Publisher() { PublisherId = 3, Name = "Nowa Era", EstablishedYear = 1992 }
        );

        // wprowadzamy odpowiednie rekordy do bazy
        modelBuilder.Entity<Book>().HasData(
            new Book() { Id = 1, Title = "Pan Tadeusz", Author = "Adam Mickiewicz", YearOfPublishing = 1834, ISBN = "1111111111", PageNumber = 300, PublisherId = 1 },
            new Book() { Id = 2, Title = "Lalka", Author = "Bolesław Prus", YearOfPublishing = 1890, ISBN = "2222222222", PageNumber = 600, PublisherId = 1 },
            new Book() { Id = 3, Title = "C# Pro", Author = "K.K. Martin", YearOfPublishing = 1990, ISBN = "3333333333", PageNumber = 800, PublisherId = 2 }
        );
        
        // przykładowa organizacja – żeby mieć coś w tabeli Organization
        modelBuilder.Entity<Organization>().HasData(
            new Organization() { Id = 1, Name = "WSEI", Description = "Uczelnia" }
        );
    }
}