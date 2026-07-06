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
            // todo add authors cache (and possibility to share authors list between different bookstores)
            Storage.AddBook(new Book(bookTitle, new Author(authorName), pageCount));
        }

        public (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart)
        {
            var result = Storage.FindBooks(namePart);
            if (result.Any())
            {
                var book = result.First();
                return (book.Title, book.Author.Name, book.PageCount);
            }
            throw new KeyNotFoundException();
        }

        public void SortByTitleAuthor(bool ascending = true)
        {
            Storage.Sort(BookComparerFactory.StandardComparer);
        }
    }
}
