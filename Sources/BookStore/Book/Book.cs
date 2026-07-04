using System;
using System.Drawing;

namespace BookStore
{

    //:IEnumerable
    public interface IBook : IComparable<IBook> //: IEquatable<IBook>
    {
        string Name { get; }
        IAuthor Author { get; }
        int PageCount { get; }

        bool Equals(IBook? other);
    }

    public static class IBookExtensions
    {
        //public static bool operator ==(this IBook? left, IBook? right) => Equals(left, right); //EqualityComparer<Book>.Default.Equals(left, right);
        //public static bool operator !=(this IBook? left, IBook? right) => !(left == right);


/// /       public static string GetFormattedName(this IBook employee)
   //     {
    //        return $"Name: {employee.GetName().ToUpper()}";
     //   }
    }

    public class Book : IBook
    {
        public string Name { get; }
        public IAuthor Author { get; }
        public int PageCount { get; }

        public Book(string name, IAuthor author, int pageCount)
        {
            Name = name;
            Author = author;
            PageCount = pageCount;
        }

        public bool Equals(IBook? other)
        {
            if (other is null) return false;
            return Name == other.Name && Author == other.Author && PageCount == other.PageCount;
        }

        public override bool Equals(object? obj) => Equals(obj as IBook);
        public override int GetHashCode() => HashCode.Combine(Name, Author, PageCount);

        public static bool operator ==(Book? left, IBook? right) => Equals(left, right); //EqualityComparer<Book>.Default.Equals(left, right);
        public static bool operator !=(Book? left, IBook? right) => !(left == right);

        public int CompareTo(IBook? other)
        {
            return BookComparerFactory.StandardComparer.Compare(this, other);
        }
    }

}
