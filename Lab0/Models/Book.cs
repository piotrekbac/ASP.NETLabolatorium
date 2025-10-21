using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Models;

public class Book
{
    [HiddenInput]
    public int Id { get; set; }
    
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string? Title { get; set; }
    
    [StringLength(maximumLength: 350, MinimumLength = 2)]
    public string? Author { get; set; } 
    
    [Range(minimum:1, maximum: 10000)]
    public int PageNumber { get; set; }
    
    [Required]
    [StringLength(maximumLength: 350, MinimumLength = 5)]
    public string? ISBN { get; set; }
    
    [Range(minimum:-350, maximum: 3500)]
    public int YearOfPublishing { get; set; }
    
    [StringLength(maximumLength: 80, MinimumLength = 1)]
    public string? Publisher { get; set; }
    
}