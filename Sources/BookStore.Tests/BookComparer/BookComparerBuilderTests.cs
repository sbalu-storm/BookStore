using BookStore.BookComparer;
using BookStore.Models;
using System.Reflection;

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

namespace BookStore.Tests.BookComparer
{
    public static class BookStoreTests
    {
        public enum ComparisonResult
        {
            LessThan = -1,
            Equal = 0,
            GreaterThan = 1
        }

        [Fact]
        public static void SortBy_ShouldContainAllIBookProperties()
        {
            //Arrange
            var enumValues = Enum.GetNames<BookComparerBuilder.SortBy>();

            Type iBookType = typeof(IBook);
            PropertyInfo[] iBookProperties = iBookType
                .GetProperties()
                .ToArray();

            //Act
            foreach (var property in iBookProperties)
            {
                //Assert
                Assert.Contains(property.Name, enumValues);
            }
        }

        public static IEnumerable<object[]> TestCases_BooksEqual =>
            [
                [
                    "Title1", "Author1", 100, ComparisonResult.Equal, "Title1", "Author1", 100
                ],
            ];

        public static IEnumerable<object[]> TestCases_StandardComparer_AuthorEffective =>
            [
                [ "Title1", "Author1", 100, ComparisonResult.LessThan, "Title1", "Author2", 100 ],
                [ "Title2", "Author1", 100, ComparisonResult.LessThan, "Title1", "Author2", 100 ],
                [ "Title1", "Author1", 200, ComparisonResult.LessThan, "Title1", "Author2", 100 ],
                [ "Title2", "Author1", 200, ComparisonResult.LessThan, "Title1", "Author2", 100 ],

                [ "Title2",      null,   0, ComparisonResult.LessThan, "Title1", "Author2", 100 ],
                [ "Title2",        "", 200, ComparisonResult.LessThan, "Title1", "Author2", 0 ],

                [ "Title1", "Author2", 100, ComparisonResult.GreaterThan, "Title1", "Author1", 100 ],
                [ "Title1", "Author2", 100, ComparisonResult.GreaterThan, "Title2", "Author1", 100 ],
                [ "Title1", "Author2", 100, ComparisonResult.GreaterThan, "Title1", "Author1", 200 ],
                [ "Title1", "Author2", 100, ComparisonResult.GreaterThan, "Title2", "Author1", 200 ],
            ];

        public static IEnumerable<object[]> TestCases_StandardComparer_TitleEffective =>
            [
                [ "Title1", "Author1", 100, ComparisonResult.LessThan, "Title2", "Author1", 100 ],
                [ "Title1", "Author1", 100, ComparisonResult.LessThan, "Title2", "Author1", 200 ],
                [ "Title1", "Author1", 200, ComparisonResult.LessThan, "Title2", "Author1", 100 ],

                [     null, "Author1", 100, ComparisonResult.LessThan, "Title2", "Author1", 100 ],
                [     null, "Author1", 100, ComparisonResult.LessThan, "Title2", "Author1", 200 ],
                [     null, "Author1", 200, ComparisonResult.LessThan, "Title2", "Author1", 100 ],

                [ "Title2", "Author1", 100, ComparisonResult.GreaterThan, "Title1", "Author1", 100 ],
                [ "Title2", "Author1", 100, ComparisonResult.GreaterThan, "Title1", "Author1", 200 ],
                [ "Title2", "Author1", 200, ComparisonResult.GreaterThan, "Title1", "Author1", 100 ],

                [ "Title2", "Author1", 100, ComparisonResult.GreaterThan,     null, "Author1", 100 ],
                [ "Title2", "Author1", 100, ComparisonResult.GreaterThan,     null, "Author1", 200 ],
                [ "Title2", "Author1", 200, ComparisonResult.GreaterThan,     null, "Author1", 100 ],
            ];

        public static IEnumerable<object[]> TestCases_StandardComparer_PageCountEffective =>
            [
                [ "Title1", "Author1", 100, ComparisonResult.LessThan,    "Title1", "Author1", 200 ],
                [ "Title1", "Author1", 200, ComparisonResult.GreaterThan, "Title1", "Author1", 100 ],
            ];

        [Theory]
        [MemberData(nameof(TestCases_BooksEqual))]
        [MemberData(nameof(TestCases_StandardComparer_TitleEffective))]
        [MemberData(nameof(TestCases_StandardComparer_AuthorEffective))]
        [MemberData(nameof(TestCases_StandardComparer_PageCountEffective))]
        public static void StandardComparer_ShouldHandleAllFieldsComparsion(
            string bookName1, string authorName1, int pageCount1,
            ComparisonResult expectedResult,
            string bookName2, string authorName2, int pageCount2
            )
        {
            //Arrange
            Author author1 = new Author(authorName1);
            Author author2 = new Author(authorName2);

            IBook book1 = new Book(bookName1, author1, pageCount1);
            IBook book2 = new Book(bookName2, author2, pageCount2);

            var comparer = BookComparerFactory.StandardComparer;

            //Act
            var actualResult = comparer.Compare(book1, book2);

            //Assert
            Assert.Equal((int)expectedResult, actualResult);
        }

        public static IEnumerable<object[]> TestCases_BasicComparerCasesByTitle =>
            [
                [
                    new Book("Title2", new Author("Author2"), 200),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    (new BookComparerBuilder()).ByName().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title2", new Author("Author2"), 200),
                    new BookComparerBuilder().ByName(false).Build()
                ],
                [
                    new Book("Title2", new Author("Author0"), 20),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    new BookComparerBuilder().ByName().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title2", new Author("Author0"), 20),
                    new BookComparerBuilder().ByName(false).Build()
                ],
            ];

        public static IEnumerable<object[]> TestCases_BasicComparerCasesByAuthor =>
            [
                [
                    new Book("Title2", new Author("Author2"), 200),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    new BookComparerBuilder().ByAuthor().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title2", new Author("Author2"), 200),
                    new BookComparerBuilder().ByAuthor(false).Build()
                ],
                [
                    new Book("Title0", new Author("Author2"), 20),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    new BookComparerBuilder().ByAuthor().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title0", new Author("Author2"), 20),
                    new BookComparerBuilder().ByAuthor(false).Build()
                ],
            ];

        public static IEnumerable<object[]> TestCases_BasicComparerCasesByPageCount =>
            [
                [
                    new Book("Title2", new Author("Author2"), 200),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    new BookComparerBuilder().ByPageCount().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title2", new Author("Author2"), 200),
                    new BookComparerBuilder().ByPageCount(false).Build()
                ],
                [
                    new Book("Title0", new Author("Author0"), 200),
                    ComparisonResult.GreaterThan,
                    new Book("Title1", new Author("Author1"), 100),
                    new BookComparerBuilder().ByPageCount().Build(),
                ],
                [
                    new Book("Title1", new Author("Author1"), 100),
                    ComparisonResult.GreaterThan,
                    new Book("Title0", new Author("Author0"), 200),
                    new BookComparerBuilder().ByPageCount(false).Build()
                ],
            ];

        [Theory]
        [MemberData(nameof(TestCases_BasicComparerCasesByTitle))]
        [MemberData(nameof(TestCases_BasicComparerCasesByAuthor))]
        [MemberData(nameof(TestCases_BasicComparerCasesByPageCount))]
        public static void BookComparerBuilder_ShouldHandleOrderCorrectly(
            IBook book1,
            ComparisonResult expectedResult,
            IBook book2,
            IComparer<IBook> comparer
            )
        {
            //Arange
            //Act
            var actualResult = comparer.Compare(book1, book2);

            //Assert
            Assert.Equal((int)expectedResult, actualResult);
        }
    }
}
