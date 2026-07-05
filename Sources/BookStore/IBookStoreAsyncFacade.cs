using BookStore.BookComparer;
using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;
using System.Xml.Linq;

namespace BookStore
{
    interface IBookStoreAsyncFacade
    {
        Task AddBooksFromXmlAsync(string filePath, CancellationToken ct, bool configureAwait = false);
        Task ExportToXmlAsync(string filePath, CancellationToken ct, bool configureAwait = false);

        Task AddBookAsync(string bookTitle, string authorName, int pageCount);

        Task<(string BookTitle, string AuthorName, int PageCount)> FindBookAsync(string namePart, CancellationToken ct, bool configureAwait = false);

        Task SortAsync(string namePart, CancellationToken ct, bool configureAwait = false);
    }
}
