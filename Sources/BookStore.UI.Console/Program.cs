using BookStore.BookComparer;
using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;

namespace BookStore.UI.Teminal
{
    class Program
    {
        static void FacadeApiUsageSample()
        {
            try
            {
                BookStoreFacade bsf = new("testSource.xml");
                bsf.AddBook("Hyperion", "Dan Simmons", 700);
                bsf.AddBook("Hyperion", "John Kits", 300);
                bsf.AddBook("Mastadonia", "Clifford Simak", 500);
                bsf.AddBook("The ring around the sun", "Clifford Simak", 500);
                bsf.AddBook("Clean Code", "Martin", 672);
                bsf.AddBook("Clean Code", "Martin", 464);
                bsf.AddBook("Clean Code", "Martin", 463);
                bsf.AddBook("Clean Code", "Martin", 464);

                bsf.AddBooksFromXml("testSource.xml");
                bsf.ExportToXml("outputFacadepleApi.xml");

                PrintBooksFromXml("FacadeApiUsageSample", "outputFacadepleApi.xml");

                bsf.SortByTitleAuthor();

                bsf.ExportToXml("outputFacadepleApi.xml");

                PrintBooksFromXml("FacadeApiUsageSample - Sorted", "outputFacadepleApi.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("FacadeApiUsageSample - Data process error " + ex.Message.ToString());
            }
        }

        static void SimpleApiUsageSample()
        {
            try
            {
                BookStoreSimple bss = new("testSource.xml");
                bss.AddBook("Hyperion", "Dan Simmons", 700);
                bss.AddBook("Hyperion", "John Kits", 300);
                bss.AddBook("Mastadonia", "Clifford Simak", 500);
                bss.AddBook("The ring around the sun", "Clifford Simak", 500);
                bss.AddBook("Clean Code", "Martin", 672);
                bss.AddBook("Clean Code", "Martin", 464);
                bss.AddBook("Clean Code", "Martin", 463);
                bss.AddBook("Clean Code", "Martin", 464);

                bss.AddBooksFromXml("testSource.xml");
                bss.ExportToXml("outputSimpleApi.xml");

                PrintBooksFromXml("SimpleApiUsageSample", "outputSimpleApi.xml");

                bss.SortByTitleAuthor();

                bss.ExportToXml("outputSimpleApi.xml");

                PrintBooksFromXml("SimpleApiUsageSample - Sorted", "outputSimpleApi.xml");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SimpleApiUsageSample - Data process error " + ex.Message.ToString());
            }
        }

        static void PrintBooksFromXml(string title, string xmlFilePath)
        {
            IBookStore bs = new InMemoryBookStore();
            bs.AddBooks(new XmlBookStoreSource(xmlFilePath));

            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();
            bs.Export(new ConsoleBookStoreExporter());
            Console.WriteLine();
        }

        static void LowLevelApiUsageSample()
        {
            try
            {
                //todo
                //AuthorCache authorCache = new AuthorCache();
                //AuthorCache.Resolve("Fawler");

                var books = new Book[]
                {
                    new("Refactoring", new Author("Fawler"), 432),
                    new("Clean Code", new Author("Martin"), 672),
                    new("Clean Code", new Author("Martin"), 464),
                    new("Clean Code", new Author("Martin"), 463),
                    new("Clean Code", new Author("Martin"), 464)
                };

                IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));

                bs.AddBook(new Book(" ", new Author("Empty Title"), 1243));
                bs.AddBook(new Book("Null author", null, 1243));
                //bs.AddBook(new Book("Book7", null, -1));

                bs.AddBook(new Book("Jonatan Livingston Seagull", new Author("Richard Bach"), 122));
                bs.AddBook(new Book("Hyperion", new Author("Dan Simmons"), 700));
                bs.AddBook(new Book("Hyperion", new Author("John Kits"), 300));
                bs.AddBook(new Book("Mastadonia", new Author("Clifford Simak"), 500));
                bs.AddBook(new Book("The ring around the sun", new Author("Clifford Simak"), 500));

                bs.AddBook(new Book("Book7", new Author("Author0"), 1243));
                bs.AddBook(new Book("Book7", new Author("Author0"), 1234));
                bs.AddBook(new Book("Book7", new Author("Author"), 1234));
                bs.AddBook(new Book("Book7", new Author("Author2"), 1234));
                bs.AddBook(new Book("Book3", new Author("Author1"), 1234));
                bs.AddBook(new Book("Book2", new Author("Author2"), 1234));
                bs.AddBook(new Book("Book5", new Author("Author3"), 1234));

                bs.AddBooks(new XmlBookStoreSource("testSource.xml"));

                Console.WriteLine();
                Console.WriteLine("LowLevelApiUsageSample");
                Console.WriteLine();
                bs.Export(new ConsoleBookStoreExporter());
                Console.WriteLine();

                //bs.Sort();
                //bs.Sort((new BookComparerBuilder()).ByPageCount().ByName().ByAuthor().Build());
                
                bs.Sort(BookComparerFactory.StandardComparer);
                //bs.Sort(BookComparerFactory.DescendingComparer);

                List<IBook> exportedBookList = new List<IBook>();

                bs.Export(new ArrayBookStoreExporter(ref exportedBookList));

                bs.Export(new XmlBookStoreExporter("outputlowLevelApi.xml"));

                Console.WriteLine();
                Console.WriteLine("LowLevelApiUsageSample - Sorted");
                Console.WriteLine();
                bs.Export(new ConsoleBookStoreExporter());
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("LowLevelApiUsageSample - Data process error " + ex.Message.ToString());
            }
        }

        static void Main(string[] args)
        {
            LowLevelApiUsageSample();

            SimpleApiUsageSample();

            FacadeApiUsageSample();

            Console.ReadLine();
        }
    }
}