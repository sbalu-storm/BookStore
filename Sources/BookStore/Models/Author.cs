namespace BookStore.Models
{
    public interface IAuthor : IComparable<IAuthor>
    {
        string Name { get; }

        bool Equals(IAuthor? other);

        private static readonly Lazy<IAuthor> _default = new(() => new Author(""));
        public static IAuthor Default => _default.Value;

        static int Compare(IAuthor x, IAuthor y)
        {
            if (x == null) 
                x = Default;
            if (y == null) 
                y = Default;

            return x.CompareTo(y);
        }
    }

    public class Author : IAuthor
    {
        public string Name { get; }

        public Author(string name)
        {
            if (name == null)
                Name = "";
            else
                Name = name;
        }

        public bool Equals(IAuthor? other)
        {
            if (other == null)
                other = IAuthor.Default;

            return Name == other.Name;
        }

        public override bool Equals(object? obj) => Equals(obj as IAuthor);
        public override int GetHashCode() => string.GetHashCode(Name);

        public static bool operator ==(Author? left, IAuthor? right) => Equals(left, right);
        public static bool operator !=(Author? left, IAuthor? right) => !(left == right);

        public int CompareTo(IAuthor? other)
        {
            if (other == null)
                other = IAuthor.Default;

            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

    }

}
