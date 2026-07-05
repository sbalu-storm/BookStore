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

        Task<(string BookTitle, string AuthorName, int PageCount)> FindBookAsync(string namePart, CancellationToken ct, bool configureAwait = false);

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




    class BookStoreSimpleFacade : IBookStoreFacade
    {
        private List<(string BookTitle, string AuthorName, int PageCount)> _storage = [];

        public BookStoreSimpleFacade(string xmlFilePath)
        {
            AddBooksFromXml(xmlFilePath);
        }

        public void AddBooksFromXml(string xmlFilePath)
        {
            var doc = XDocument.Load(xmlFilePath, LoadOptions.PreserveWhitespace);

            doc.Descendants("BOOK")
                .Select(b => ( 
                    (string)b.Element("TITLE"), 
                    (string)b.Element("AUTHOR"), 
                    (int)b.Element("PAGECOUNT")
                    ) );
        }

        public void ExportToXml(string xmlFilePath)
        {
            XElement xmlTree = new XElement("LIBRARY",
                from book in _storage
                select new XElement("BOOK",
                    new XElement("TITLE", book.BookTitle),
                    new XElement("AUTHOR", book.AuthorName),
                    new XElement("PAGECOUNT", book.PageCount)
                )
            );
            xmlTree.Save(xmlFilePath);
        }

        public void AddBook(string bookTitle, string authorName, int pageCount)
        {
            _storage.Add( (bookTitle, authorName, pageCount ) );
        }

        public (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart)
        {
            return _storage.Where(x => x.BookTitle.Contains(namePart, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public void SortByTitleAuthor(bool ascending = true)
        {
            if (ascending)
            {
                _storage = _storage
                    .OrderBy(e => e.BookTitle)
                    .ThenBy(e => e.AuthorName)
                    .ThenBy(e => e.PageCount)
                    .ToList();
            }
            else
            {
                _storage = _storage
                    .OrderByDescending(e => e.BookTitle)
                    .ThenByDescending(e => e.AuthorName)
                    .ThenByDescending(e => e.PageCount)
                    .ToList();
            }
        }

    }






}
