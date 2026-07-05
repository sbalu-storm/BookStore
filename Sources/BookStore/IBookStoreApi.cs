namespace BookStore
{
    interface IBookStoreApi
    {
        void AddBooksFromXml(string filePath);
        void ExportToXml(string filePath);

        void AddBook(string bookTitle, string authorName, int pageCount);

        (string BookTitle, string AuthorName, int PageCount) FindFirstBook(string namePart);

        void SortByTitleAuthor(bool ascending = true);
    }
}
