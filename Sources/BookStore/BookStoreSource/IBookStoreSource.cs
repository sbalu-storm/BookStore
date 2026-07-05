using BookStore.Models;

namespace BookStore.BookStoreSource
{
    public interface IBookStoreSource
    {
        IEnumerable<IBook> GetAllBooks();
    }
}
