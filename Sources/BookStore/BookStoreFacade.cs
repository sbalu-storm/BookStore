using BookStore.BookComparer;
using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;

namespace BookStore
{
    public class BookStoreFacade: IBookStoreApi
    {
        private IBookStore Storage { get; }

        public BookStoreFacade(string xmlFilePath)
        {
            Storage = new InMemoryBookStore();
            //todo add empty version - its required for async loading
            AddBooksFromXml(xmlFilePath);
        }

        public void AddBooksFromXml(string xmlFilePath)
        {
            Storage.AddBooks(new XmlBookStoreSource(xmlFilePath));
        }

        public void ExportToXml(string xmlFilePath)
        {
            Storage.Export(new XmlBookStoreExporter(xmlFilePath));
        }

        public void AddBook(string bookTitle, string authorName, int pageCount)
        {
            Storage.AddBook(new Book(bookTitle, new Author(authorName), pageCount));
        }

        public (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart)
        {
            var result = Storage.FindBooks((IBook book) => book.Name.IndexOf(namePart) != -1); //todo cultureInfo
            if (result.Any())
            {
                var book = result.First();
                return (book.Name, book.Author.Name, book.PageCount);
            }
            throw new KeyNotFoundException();
        }

        public void SortByTitleAuthor(bool ascending = true)
        {
            Storage.Sort(BookComparerFactory.StandardComparer);
        }
    }
}
