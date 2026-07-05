using System.Xml.Linq;

namespace BookStore
{
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
