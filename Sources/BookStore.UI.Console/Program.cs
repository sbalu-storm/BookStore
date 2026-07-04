using BookStore;
using System.Xml.Linq;
/*
static class IBookExtensions
{
    public static void Randomize(this IBookStore bookStore)
    {
        bookStore.Bo
        Random.Shared.Shuffle(numbers);
    }
}*/

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

        bs.AddBook(new Book("Book7", new Author("Author0"), 1243));
        bs.AddBook(new Book("Book7", new Author("Author0"), 1234));
        bs.AddBook(new Book("Book7", new Author("Author"), 1234));
        bs.AddBook(new Book("Book7", new Author("Author2"), 1234));
        bs.AddBook(new Book("Book3", new Author("Author1"), 1234));
        bs.AddBook(new Book("Book2", new Author("Author2"), 1234));
        bs.AddBook(new Book("Book5", new Author("Author3"), 1234));

        //todo randomize provider (Sort by hash?)

        bs.Sort();

        // todo : buld sorter

        //bool ascending = true;
        //bs.Sort((IBook left, IBook right) => ascending ? left.CompareTo(right) : -left.CompareTo(right));

        //bs.Sort((IBook left, IBook right) => Random.Shared.Next(-1,1) );


        bs.Sort((new BookComparerBuilder()).ByPageCount().ByName().ByAuthor().Build());

        bs.Sort(BookComparerFactory.RandomComparer);

        bs.Sort(BookComparerFactory.StandardComparer);

        bs.Sort(BookComparerFactory.DescendingComparer);


        //bs.SortByTitleAuthor();  // todo Chained methods sorter build 

        // testing policy: 
        // every public method should have a test
        // operators should be tested as Interface and as Implementation



        // todo create public facade test profider enums
        // internal structures should be hidden from external user, it allows us more flexibility in changing our external structures.
        //
        // otherwise, if the user choose to uncover our internal infrastructure, anyway we will provide two interfaces highlevel and lowlevel.



        List<IBook> booksEx = new List<IBook>();

        bs.Export(new ArrayBookStoreExporter(ref booksEx));

        

        bs.Export(new ConsoleBookStoreExporter());

        Console.ReadLine();


        //ToDo RandomOrder Adapter
    }
}