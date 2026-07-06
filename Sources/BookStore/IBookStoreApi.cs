namespace BookStore
{
    public interface IBookStoreApi
    {
        private static readonly Lazy<(string BookTitle, string AuthorName, int PageCount)> _default = new(() => new (null, null, 0));
        public static (string BookTitle, string AuthorName, int PageCount) EmptyBook => _default.Value;

        void AddBooksFromXml(string filePath);
        void ExportToXml(string filePath);

        void AddBook(string bookTitle, string authorName, int pageCount);

        (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart);

        IEnumerable<(string BookTitle, string AuthorName, int PageCount)> FindBooks(string namePart);

        void SortByTitleAuthor(bool ascending = true);
    }
}
