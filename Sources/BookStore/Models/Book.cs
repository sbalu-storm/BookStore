using BookStore.BookComparer;

namespace BookStore.Models
{
    public interface IBook : IComparable<IBook> 
    {
        string Title { get; }
        IAuthor Author { get; }
        int PageCount { get; }

        bool Equals(IBook? other);
    }

    public class Book : IBook
    {
        public string Title { get; }
        public IAuthor Author { get; }
        public int PageCount { get; }

        public Book(string title, IAuthor author, int pageCount)
        {
            if (title == null)
                Title = "";
            else
                Title = title;

            if (author == null)
                Author = IAuthor.Default;
            else
                Author = author;
            PageCount = pageCount;

            if (pageCount < 0)
                throw new InvalidDataException("Page count cannot be negative");
        }

        public bool Equals(IBook? other)
        {
            if (other is null) return false;
            return Title == other.Title && Author == other.Author && PageCount == other.PageCount;
        }

        public override bool Equals(object? obj) => Equals(obj as IBook);
        public override int GetHashCode() => HashCode.Combine(Title, Author, PageCount);

        public static bool operator ==(Book? left, IBook? right) => Equals(left, right);
        public static bool operator !=(Book? left, IBook? right) => !(left == right);

        public int CompareTo(IBook? other)
        {
            return BookComparerFactory.StandardComparer.Compare(this, other);
        }

        public override string ToString()
        {
            return $"{Title} {Author} {PageCount}";
        }
    }

}
