using BookStore.Models;

namespace BookStore.BookStoreExporter
{
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
}
