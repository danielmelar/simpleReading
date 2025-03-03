namespace simpleReading.Service;

public class BookService
{
    private int Id = 0;
    private List<Book> books = new List<Book>();

    public List<Book> GetBooks() => books;

    public void AddBook(Book book)
    {
        while (books.Exists(x => x.Id == Id))
        {
            Id++;
        }

        book.Id = Id;

        books.Add(book);
    }

    public void RemoveBook(int id)
    {
        var book = books.Find(x => x.Id == id);
        books.Remove(book);
    }
}