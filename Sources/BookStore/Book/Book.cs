namespace BookStore
{

    //:IEnumerable
    public interface IBook
    {
        string Name { get; }
        IAuthor Author { get; }
        int PageCount { get; }
    }

    public class Book : IBook
    {
        public Book(string name, IAuthor author, int pageCount)
        {
            Name = name;
            Author = author;
            PageCount = pageCount;
        }

        public string Name { get; }
        public IAuthor Author { get; }
        public int PageCount { get; }
    }

}
