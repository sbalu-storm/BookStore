using BookStore.BookComparer;
using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;
using System.Xml.Linq;

namespace BookStore
{
    interface IBookStoreFacade
    {
        void AddBooksFromXml(string filePath);
        void ExportToXml(string filePath);

        void AddBook(string bookTitle, string authorName, int pageCount);

        (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart);

        void SortByTitleAuthor(bool ascending = true);
    }
}
