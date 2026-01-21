using Library.ApplicationCore;
using Library.ApplicationCore.Entities;

namespace Library.Infrastructure.Data;

public class JsonBookRepository : IBookRepository
{
    private readonly JsonData _jsonData;

    public JsonBookRepository(JsonData jsonData)
    {
        _jsonData = jsonData;
    }

    public async Task<List<Book>> SearchBooks(string searchInput)
    {
        await _jsonData.EnsureDataLoaded();

        List<Book> searchResults = new List<Book>();
        foreach (Book book in _jsonData.Books!)
        {
            if (book.Title.Contains(searchInput, StringComparison.OrdinalIgnoreCase) ||
                book.Author?.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase) == true)
            {
                searchResults.Add(book);
            }
        }
        searchResults.Sort((b1, b2) => String.Compare(b1.Title, b2.Title));

        return searchResults;
    }
}