using System.Xml.Linq;
using BookStore.Models;

namespace BookStore.BookStoreExporter
{
    public class XmlBookStoreExporter : IBookStoreExporter
    {
        string _xmlFileName;

        public XmlBookStoreExporter(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
        }

        public void Export(IEnumerable<IBook> books)
        {
            XElement xmlTree = new XElement("LIBRARY",
                from book in books
                select new XElement("BOOK",
                    new XElement("TITLE", book.Name),
                    new XElement("AUTHOR", book.Author?.Name),
                    new XElement("PAGECOUNT", book.PageCount)
                )
            );
            xmlTree.Save(_xmlFileName);
        }
    }
}
