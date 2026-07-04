using BookStore;
using System.Xml.Linq;


class Program
{
    static void Main(string[] args)
    {
        //var authors = 

        var books = new Book[]
        {
        new("Book1", new Author("Author1"), 123),
        new("Book2", new Author("Author1"), 123),
        new("Book3", new Author("Author2"), 123)
        };

        IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));

        bs.AddBook(new Book("Book7", new Author("Author0"), 1234));
        bs.AddBook(new Book("Book7", new Author("Author0"), 1234));

        List<IBook> booksEx = new List<IBook>();

        bs.Export(new ArrayBookStoreExporter(ref booksEx));




        //ToDo RandomOrder Adapter
    }
}