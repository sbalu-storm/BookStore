using BookStore.Models;

namespace BookStore.BookComparer
{
    public static class BookComparerFactory
    {
        private static Lazy<IComparer<IBook>> _standardComparer =
            new(() => new BookComparerBuilder().ByName().ByAuthor().ByPageCount().Build());
        private static Lazy<IComparer<IBook>> _descendingComparer =
            new(() => new BookComparerBuilder().ByName(false).ByAuthor(false).ByPageCount(false).Build());

        private static Lazy<IComparer<IBook>> _randomComparer =
            new(() => Comparer<IBook>.Create((IBook x, IBook y) => Random.Shared.Next(-1, 1)) ); // todo check if it is ok

        public static IComparer<IBook> StandardComparer => _standardComparer.Value;
        public static IComparer<IBook> DescendingComparer => _descendingComparer.Value;

        public static IComparer<IBook> RandomComparer => _randomComparer.Value;
    }
}
