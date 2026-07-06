using BookStore.Models;

namespace BookStore.BookStoreExporter
{
    public class ConsoleBookStoreExporter : IBookStoreExporter
    {
        public void Export(IEnumerable<IBook> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine($"Author: {book?.Author?.Name,-25}\tTitle: {book?.Title,-25}\tPages: {book?.PageCount}");
            }
        }
    }
}
