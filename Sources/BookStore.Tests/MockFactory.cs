using BookStore.BookStoreSource;
using BookStore.Models;
using Moq;

namespace BookStore.Tests
{
    internal static class MockFactory
    {
        public static Mock<IBookStoreSource> GetBookStoreSource(IEnumerable<IBook> books)
        {
            var mockBookStoreSource = new Mock<IBookStoreSource>();

            mockBookStoreSource
                .Setup(c => c.GetAllBooks())
                .Returns(books);

            return mockBookStoreSource;
        }
    }
}
