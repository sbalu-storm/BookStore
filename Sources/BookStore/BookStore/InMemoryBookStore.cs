using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;

namespace BookStore.BookStore
{
    public class InMemoryBookStore : IBookStore
    {
        List<IBook> Books { get; } = [];

        public InMemoryBookStore()
        {
        }

        public InMemoryBookStore(IBookStoreSource bookStoreSource) 
        {
            AddBooks(bookStoreSource);
        }

        public void AddBook(IBook book)
        {
            Books.Add(book);
        }

        public void AddBooks(IEnumerable<IBook> books)
        {
            Books.AddRange(books);
        }

        public void AddBooks(IBookStoreSource bookStoreSource)
        {
            var books = bookStoreSource.GetAllBooks().ToList();
            Books.AddRange(books);
        }

        public void Export(IBookStoreExporter exporter)
        {
            exporter.Export(Books);
        }

        public IEnumerable<IBook> FindBooks(Func<IBook, bool> comparer)
        {
            foreach (var book in Books)
            {
                if (comparer(book))
                {
                    yield return book;
                }
            }
        }

        public IEnumerable<IBook> FindBooks(string titlePart)
        {
            return FindBooks((IBook book) => book.Title.Contains(titlePart, StringComparison.InvariantCulture));
        }


        public void Sort()
        {
            Books.Sort();
        }

        public void Sort(IComparer<IBook> comparer)
        {
            Books.Sort(comparer);
        }

    }

}
