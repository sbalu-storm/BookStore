using System.Xml.Linq;
using BookStore.Models;

namespace BookStore.BookStoreSource
{
    public class XmlBookStoreSource : IBookStoreSource
    {
        XDocument _doc;

        public XmlBookStoreSource(string xmlFilePath) 
        {
            _doc = XDocument.Load(xmlFilePath, LoadOptions.PreserveWhitespace);
        }

        public XmlBookStoreSource(Stream stream)
        {
            _doc = XDocument.Load(stream, LoadOptions.PreserveWhitespace);
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _doc.Descendants("BOOK")
                .Select(b => new Book(
                (string)b.Element("TITLE"),
                new Author((string)b.Element("AUTHOR")),
                (int)b.Element("PAGECOUNT")
                ));
        }
    }
}
