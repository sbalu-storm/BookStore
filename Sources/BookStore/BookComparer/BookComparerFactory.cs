using BookStore.Models;

namespace BookStore.BookComparer
{
    public static class BookComparerFactory
    {
        private static Lazy<IComparer<IBook>> _standardComparer =
            new(() => new BookComparerBuilder().ByAuthor().ByName().ByPageCount().Build());
        private static Lazy<IComparer<IBook>> _descendingComparer =
            new(() => new BookComparerBuilder().ByAuthor(false).ByName(false).ByPageCount(false).Build());

        public static IComparer<IBook> StandardComparer => _standardComparer.Value;
        public static IComparer<IBook> DescendingComparer => _descendingComparer.Value;
    }
}
