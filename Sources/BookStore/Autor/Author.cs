namespace BookStore
{
    public interface IAuthor
    {
        string Name { get; }
    }

    // Equals, GUID, == 
    // IDisposible ? 

    public class Author : IAuthor
    {
        public Author(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

}
