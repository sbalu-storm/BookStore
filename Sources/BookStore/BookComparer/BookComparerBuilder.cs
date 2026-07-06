using BookStore.Models;

namespace BookStore.BookComparer
{
    // todo: make scalable when we would have Superbook with rating or sub-books or illustrations
    // so adding new field or IBook implementation should affect
    // only BookComparerFactory, but not BookComparerBuilder
    // something alike LINQ search builder
    public class BookComparerBuilder 
    {
        public enum SortBy
        {
            Title,
            Author,
            PageCount
        }

        List<(SortBy Field, bool Ascending)> SortOrder { get; } = [];

        public BookComparerBuilder ByName(bool ascending = true)
        {
            SortOrder.Add((SortBy.Title, ascending));
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
                    case SortBy.Title: result = string.Compare(x.Title, y.Title); break;
                    case SortBy.Author: result = IAuthor.Compare(x.Author, y.Author); break;
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
