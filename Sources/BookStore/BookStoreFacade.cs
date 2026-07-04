using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore;

namespace BookStore
{
    interface IBookStoreFacade
    {
        void AddBooksFromXml(string filePath);
        void ExportToXml(string filePath);

        void AddBook(string bookTitle, string authorName, int pageCount);

        (string bookTitle, string authorName, int pageCount) FindFirstBook(string namePart);

        void SortByTitleAuthor();


        /*
        void AddBook(IBook book);
        void AddBooks(IEnumerable<IBook> books);

        //IBook FindBook(string namePart);

        /*
        */
        //IEnumerable<IBook> FindBooks(Func<IBook, bool> comparer);
        //void Sort(Func<IBook, IBook, bool> comparer);
    }

    interface IBookStoreAsyncFacade
    {
        Task AddBooksFromXmlAsync(string filePath, CancellationToken ct, bool configureAwait = false);
        Task ExportToXmlAsync(string filePath, CancellationToken ct, bool configureAwait = false);

        Task AddBookAsync(string bookTitle, string authorName, int pageCount);

        Task<(string bookTitle, string authorName, int pageCount)> FindBookAsync(string namePart, CancellationToken ct, bool configureAwait = false);

        Task SortAsync(string namePart, CancellationToken ct, bool configureAwait = false);
/*
        void AddBookAsync(IBook book);
        void AddBooksAsync(IEnumerable<IBook> books, CancellationToken ct);
*/
    }


    class BookStoreFacade: IBookStoreFacade//, IBookStoreAsyncFacade
    {
        private BookStore.IBookStore Storage { get; }

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

        // todo: separate namespace for this facade
        // todo: add service and DI/lazy for Author cache-
        public (string bookTitle, string authorName, int pageCount) FindFirstBook(string namePart)
        {
            var result = Storage.FindBooks((IBook book) => book.Name.IndexOf(namePart) != -1); //todo cultureInfo
            if (result.Any())
            {
                var book = result.First();
                return (book.Name, book.Author.Name, book.PageCount);
            }
            return ((string)null, (string)null, -1);
            //return null;
        }

        public void SortByTitleAuthor()
        {
            Storage.Sort(

            //ICloneable
        }

    }

    // todo md
    // class diagrams
    // parameters description
}
