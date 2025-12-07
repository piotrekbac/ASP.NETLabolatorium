using Microsoft.EntityFrameworkCore;

namespace Lab0.Models;

// Klasa pomocnicza dziedzicząca po List<T>, przechowuje metadane o stronicowaniu
public class PagingListAsync<T>
{
    public IAsyncEnumerable<T> Data { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public int Page { get; }
    public int Size { get; }
    public bool IsFirst { get; }
    public bool IsLast { get; }
    public bool IsNext { get; }
    public bool IsPrevious { get; }
    
    private PagingListAsync(IAsyncEnumerable<T> data, int totalItems, int page, int size)
    {
        TotalItems = totalItems;
        Page = page;
        Size = size;
        TotalPages = CalcTotalPages(Page, TotalItems, Size);
        IsFirst = Page <= 1;
        IsLast = Page >= TotalPages;
        IsNext = !IsLast;
        IsPrevious = !IsFirst;
        Data = data;
    }

    // Definiujemmy metodę fabryczną, wykonującą asynchroniczne zapytania do bazy
    public static async Task<PagingListAsync<T>> CreateAsync(IQueryable<T> source, int page, int size)
    {
        var count = await source.CountAsync();
        var items = source.Skip((page - 1) * size).Take(size).AsAsyncEnumerable();
        return new PagingListAsync<T>(items, count, page, size);
    }

    private static int CalcTotalPages(int page, int totalItems, int size)
    {
        return totalItems / size + (totalItems % size > 0 ? 1 : 0);
    }
}