using BookStore.Models;

namespace BookStore.BookStoreExporter
{
    public interface IBookStoreExporter
    {
        void Export(IEnumerable<IBook> books);
    }
}
