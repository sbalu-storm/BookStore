using BookStore.BookComparer;
using BookStore.BookStore;
using BookStore.BookStoreExporter;
using BookStore.BookStoreSource;
using BookStore.Models;
using System.Reflection;

namespace BookStore.Tests.BookStore
{
    public static class BookStoreTests
    {
        static IBookStore GetInitialBookStore()
        {
            var books = new Book[]
            {
                    new("Refactoring", new Author("Fawler"), 432),
                    new("Clean Code", new Author("Martin"), 672),
                    new("Clean Code", new Author("Martin"), 464),
                    new("Clean Code", new Author("Martin"), 463),
                    new("Clean Code", new Author("Martin"), 464)
            };
            IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));
            bs.AddBook(new Book("Jonatan Livingston Seagull", new Author("Richard Bach"), 122));
            bs.AddBook(new Book("Hyperion", new Author("Dan Simmons"), 700));
            bs.AddBook(new Book("Hyperion", new Author("John Kits"), 300));
            bs.AddBook(new Book("Mastadonia", new Author("Clifford Simak"), 500));
            bs.AddBook(new Book("The ring around the sun", new Author("Clifford Simak"), 500));
            bs.AddBook(new Book("The Actor", new Author("Some Author"), 300));
            
            return bs;
        }

        [Fact]
        public static void BookStore_ShouldPerformCaseSensitiveSimpleSearch()
        {
            //Arrange
            IBookStore bs = GetInitialBookStore();

            //Act
            var result = bs.FindBooks("actor");

            //Assert
            Assert.Equal(1, result?.Count());
            Assert.Equal("Refactoring", result?.First()?.Title);
        }

        [Fact]
        public static void BookStore_ShouldSortStandard()
        {

            //Arrange
            Book fawlerRefactoring = new("Refactoring", new Author("Fawler"), 672);
            Book martinRefactoring = new("Refactoring", new Author("Martin"), 672);
            Book martinCleanCode = new("Clean Code", new Author("Martin"), 672);

            var books = new Book[]
            {
                    martinRefactoring,
                    martinCleanCode,
                    fawlerRefactoring,
            };
            IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));

            //Act
            bs.Sort(BookComparerFactory.StandardComparer);

            //Assert
            List<IBook> exportedBookList = new List<IBook>();
            bs.Export(new ArrayBookStoreExporter(ref exportedBookList));


            Assert.Equal(3, exportedBookList.Count);
            Assert.Equal(fawlerRefactoring, exportedBookList[0]);
            Assert.Equal(martinCleanCode, exportedBookList[1]);
            Assert.Equal(martinRefactoring, exportedBookList[2]);
        }

        [Fact]
        public static void BookStore_ShouldSortDescending()
        {

            //Arrange
            Book fawlerRefactoring = new("Refactoring", new Author("Fawler"), 672);
            Book martinRefactoring = new("Refactoring", new Author("Martin"), 672);
            Book martinCleanCode = new("Clean Code", new Author("Martin"), 672);

            var books = new Book[]
            {
                    martinRefactoring,
                    martinCleanCode,
                    fawlerRefactoring,
            };
            IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(books));

            //Act
            bs.Sort(BookComparerFactory.DescendingComparer);

            //Assert
            List<IBook> exportedBookList = new List<IBook>();
            bs.Export(new ArrayBookStoreExporter(ref exportedBookList));


            Assert.Equal(3, exportedBookList.Count);
            Assert.Equal(martinRefactoring, exportedBookList[0]);
            Assert.Equal(martinCleanCode, exportedBookList[1]);
            Assert.Equal(fawlerRefactoring, exportedBookList[2]);
        }

        [Fact]
        public static void BookStore_ShouldImportAndExport()
        {

            //Arrange
            Book fawlerRefactoring = new("Refactoring", new Author("Fawler"), 672);
            Book martinRefactoring = new("Refactoring", new Author("Martin"), 672);
            Book martinCleanCode = new("Clean Code", new Author("Martin"), 672);

            var importedBooks = new Book[]
            {
                    martinRefactoring,
                    martinCleanCode,
                    fawlerRefactoring,
            };

            //Act
            IBookStore bs = new InMemoryBookStore(new ArrayBookStoreSource(importedBooks));
            List<IBook> exportedBookList = new List<IBook>();
            bs.Export(new ArrayBookStoreExporter(ref exportedBookList));

            //Assert
            Assert.Equal(importedBooks.Length, exportedBookList.Count);
            for (int i = 0; i < importedBooks.Length; ++i)
            {
                Assert.Equal(importedBooks[i], exportedBookList[i]);
            }
        }
    }
}
