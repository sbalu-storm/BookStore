using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;

namespace BookStore.BookStore
{
    public interface IBookStore
    {
        void Export(IBookStoreExporter exporter);

        void AddBook(IBook book);
        void AddBooks(IEnumerable<IBook> books);
        void AddBooks(IBookStoreSource bookStoreSource);

        IEnumerable<IBook> FindBooks(string titlePart);
        IEnumerable<IBook> FindBooks(Func<IBook, bool> comparer);

        void Sort(IComparer<IBook> comparer);

        void Sort();
    }
}
