using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;

namespace BookStore.UI.Teminal
{

    class Program
    {
        static void Main(string[] args)
        {
            //var authors = 

            var books = new Book[]
            {
                //new("Book1", new Author("Author1"), 123),
                //new("Book2", new Author("Author1"), 123),
                //new("Book3", new Author("Author2"), 123)
            };

            IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));
            /*bs.AddBook(new Book(null, new Author("Author0"), 1243));
            bs.AddBook(new Book(" ", new Author("Author0"), 1243));
            bs.AddBook(new Book("Book7", null, 1243));
            bs.AddBook(new Book("Book7", null, -1));

            /*
            bs.AddBook(new Book("Book7", new Author("Author0"), 1243));
            bs.AddBook(new Book("Book7", new Author("Author0"), 1234));
            bs.AddBook(new Book("Book7", new Author("Author"), 1234));
            bs.AddBook(new Book("Book7", new Author("Author2"), 1234));
            bs.AddBook(new Book("Book3", new Author("Author1"), 1234));
            bs.AddBook(new Book("Book2", new Author("Author2"), 1234));
            bs.AddBook(new Book("Book5", new Author("Author3"), 1234));

            bs.Sort();
            bs.Sort((new BookComparerBuilder()).ByPageCount().ByName().ByAuthor().Build());
            bs.Sort(BookComparerFactory.RandomComparer);
            bs.Sort(BookComparerFactory.StandardComparer);

            //bs.Sort(BookComparerFactory.DescendingComparer);

            List<IBook> booksEx = new List<IBook>();

            bs.Export(new ArrayBookStoreExporter(ref booksEx));
            */

            bs.AddBooks(new XmlBookStoreSource("test.xml"));


            bs.Export(new XmlBookStoreExporter("test2.xml"));

            bs.Export(new ConsoleBookStoreExporter());
            Console.ReadLine();
        }
    }
}