using System.Xml.Linq;

namespace BookStore
{
    public interface IBookStoreExporter
    {
        void Export(IEnumerable<IBook> books);
    }

    public class ArrayBookStoreExporter : IBookStoreExporter
    {
        List<IBook> _books;

        public ArrayBookStoreExporter() 
        {
            _books = new List<IBook>();
        }

        public ArrayBookStoreExporter(ref List<IBook> booksList)
        {
            _books = booksList;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _books;
        }

        public void Export(IEnumerable<IBook> books)
        {
            _books.AddRange(books);
        }
        
    }

    public class XmlBookStoreExporter : IBookStoreExporter
    {
        string _xmlFileName;

        public XmlBookStoreExporter(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
        }

        public void Export(IEnumerable<IBook> books)
        {
            XElement xmlTree = new XElement("LIBRARY",
                from book in books
                select new XElement("BOOK",
                    new XElement("TITLE", book.Name),
                    new XElement("AUTHOR", book.Author?.Name),
                    new XElement("PAGECOUNT", book.PageCount)
                )
            );
            xmlTree.Save(_xmlFileName);
        }
    }

    public class ConsoleBookStoreExporter : IBookStoreExporter
    {
        public void Export(IEnumerable<IBook> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book?.Name}   Author: {book?.Author?.Name}   Title: {book?.PageCount}   ");
            }
        }

    }


}
