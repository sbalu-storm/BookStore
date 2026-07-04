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

    public class XmlBookStoreExporter : IBookStoreExporter//, IEnumerable<IBook>, IDisposable
    {
        public XmlBookStoreExporter(string xmlFileName)
        {
        }

        public void Export(IEnumerable<IBook> books)
        {
            throw new NotImplementedException();
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
