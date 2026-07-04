using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore
{
    // IDisposible
    // to do memory/stack optimizations 

    public interface IBookStore
    {
        void Export(IBookStoreExporter exporter);

        void AddBook(IBook book);
        void AddBooks(IEnumerable<IBook> books);
        void AddBooks(IBookStoreSource bookStoreSource);
        /*
        IBook FindBook(string namePart);
        void Sort(string namePart);
        */
        IEnumerable<IBook> FindBooks(Func<IBook, bool> comparer);
        //void Sort(Func<IBook, IBook, int> comparer);
        void Sort(IComparer<IBook> comparer);

        void Sort();
    }
    /*
    public class SQLBookStore : IBookStore
    {
    }

    public class MemcachedBookStore : IBookStore
    {
    }
    */


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

        public void Sort()
        {
            Books.Sort();
        }
        /*
        public void Sort(Func<IBook, IBook, int> comparerFunc)
        {
            var comparer_ = Comparer<IBook>.Create((x, y) => comparerFunc(x,y));
            Books.Sort(comparer_);
        }*/

        public void Sort(IComparer<IBook> comparer)
        {
            Books.Sort(comparer);

            //IComparer comparer_ = Comparer<IBook[]>.Create((x, y) => x[1].CompareTo(y[1]));

            //Books.Sort(comparer_);
        }

        //Autroh
        //todo Book == != Eq Hash IHashable IEnumerable IComparable



        //IComparer<in T> where T : allows ref struct
        //{
        //
        // Summary:
        //     Compares two objects and returns a value indicating whether one is less than,
        //     equal to, or greater than the other.
        //
        // Parameters:
        //   x:
        //     The first object to compare.
        //
        //   y:
        //     The second object to compare.
        //
        // Returns:
        //     A signed integer that indicates the relative values of x and y, as shown in the
        //     following table.
        //
        //     Value – Meaning
        //     Less than zero –x is less than y.
        //     Zero –x equals y.
        //     Greater than zero –x is greater than y.
        //int Compare(T? x, T? y);
    }

}
