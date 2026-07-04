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
        //todo add stream
        public XmlBookStoreSource(string xmlFilePath) 
        {
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            throw new NotImplementedException();
        }
    }

}
