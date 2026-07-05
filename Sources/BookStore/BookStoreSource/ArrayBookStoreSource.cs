using BookStore.Models;

namespace BookStore.BookStoreSource
{
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
}
