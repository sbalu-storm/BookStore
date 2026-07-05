using BookStore.Models;

namespace BookStore.BookStoreExporter
{
    public class ConsoleBookStoreExporter : IBookStoreExporter
    {
        public void Export(IEnumerable<IBook> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book?.Name,-25}\tAuthor: {book?.Author?.Name,-25}\tPages: {book?.PageCount}");
            }
        }
    }
}
