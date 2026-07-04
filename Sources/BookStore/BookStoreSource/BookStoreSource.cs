using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore
{
    public interface IBookStoreSource
    {
        IEnumerable<IBook> GetAllBooks();
    }

    public class ArrayBookStoreSource : IBookStoreSource
    {
        IEnumerable<IBook> _books;

        public ArrayBookStoreSource(IEnumerable<IBook> books) 
        {
            _books = books;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _books;
        }
    }



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
