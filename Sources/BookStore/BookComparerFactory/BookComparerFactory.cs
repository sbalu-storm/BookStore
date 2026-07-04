namespace BookStore
{

    public static class BookComparerFactory
    {
        private static Lazy<IComparer<IBook>> _standardComparer =
            new(() => new BookComparerBuilder().ByName().ByAuthor().ByPageCount().Build());
        private static Lazy<IComparer<IBook>> _descendingComparer =
            new(() => new BookComparerBuilder().ByName(false).ByAuthor(false).ByPageCount(false).Build());

        private static Lazy<IComparer<IBook>> _randomComparer =
            new(() => Comparer<IBook>.Create((IBook x, IBook y) => Random.Shared.Next(-1, 1)) );

        public static IComparer<IBook> StandardComparer => _standardComparer.Value;
        public static IComparer<IBook> DescendingComparer => _descendingComparer.Value;

        public static IComparer<IBook> RandomComparer => _randomComparer.Value;
    }

    // todo: make scalable when we would have Superbook with rating or sub-books or illustrations
    // so adding new field or IBook implementation should affect
    // only BookComparerFactory, but not BookComparerBuilder
    public class BookComparerBuilder 
    {
        public enum SortBy
        {
            Name,
            Author,
            PageCount
        }

        List<(SortBy Field, bool Ascending)> SortOrder { get; } = [];

        public BookComparerBuilder ByName(bool ascending = true)
        {
            SortOrder.Add((SortBy.Name, ascending));
            return this;
        }

        public BookComparerBuilder ByAuthor(bool ascending = true)
        {
            SortOrder.Add((SortBy.Author, ascending));
            return this;
        }

        public BookComparerBuilder ByPageCount(bool ascending = true)
        {
            SortOrder.Add((SortBy.PageCount, ascending));
            return this;
        }

        public BookComparerBuilder By(SortBy sortBy, bool ascending = true)
        {
            SortOrder.Add((sortBy, ascending));
            return this;
        }

        public IComparer<IBook> Build()
        {
            return Comparer<IBook>.Create(Compare);
        }

        private int Compare(IBook x, IBook y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            foreach (var sortOrder in SortOrder)
            {
                int result;
                switch (sortOrder.Field)
                {
                    case SortBy.Name: result = x.Name.CompareTo(y.Name); break;
                    case SortBy.Author: result = x.Author.CompareTo(y.Author); break;
                    case SortBy.PageCount: result = x.PageCount.CompareTo(y.PageCount); break;
                    default: result = 0; break;
                }

                if (result != 0)
                {
                    return sortOrder.Ascending ? result : -result;
                }
            }
            return 0;
        }
    }
}
