namespace BookStore.Models
{
    public interface IAuthor : IComparable<IAuthor>
    {
        string Name { get; }

        bool Equals(IAuthor? other);

        static int Compare(IAuthor x, IAuthor y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -y.CompareTo(x);
            }

            return x.CompareTo(y);
        }
    }

    public class Author : IAuthor
    {
        public string Name { get; }

        public Author(string name)
        {
            Name = name;
        }

        public bool Equals(IAuthor? other)
        {
            if (other is null) return false;
            return Name == other.Name;
        }

        public override bool Equals(object? obj) => Equals(obj as IAuthor);
        public override int GetHashCode() => string.GetHashCode(Name);

        public static bool operator ==(Author? left, IAuthor? right) => Equals(left, right);
        public static bool operator !=(Author? left, IAuthor? right) => !(left == right);

        public int CompareTo(IAuthor? other)
        {
            if (other == null) return 1;

            return Name.CompareTo(other.Name);
        }

    }

}
