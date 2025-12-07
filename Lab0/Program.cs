using Lab0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior 15 722 

var builder = WebApplication.CreateBuilder(args);

// Rejestruję serwisy w kontenerze DI
builder.Services.AddControllersWithViews();

// Konfiguruję kontekst bazy danych z wykorzystaniem SQLite
builder.Services.AddDbContext<AddDbContext>(options =>
    options.UseSqlite(@"Data Source=d:\data\Books-gr1.db"));

// Konfiguruję moduł Identity (Rejestracja oraz Logowanie)
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<AddDbContext>();

// Używam cyklu życia Transient, co oznacza nową instancję przy każdym żądaniu
builder.Services.AddTransient<IBookService, BookDbService>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// W tym miejscu uruchamiam mechanizmy uwierzytelniania oraz autoryzacji 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();