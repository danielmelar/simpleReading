namespace simpleReading.Service;

public class BookService
{
    private List<Book> books = new List<Book>();

    public List<Book> GetBooks() => books;

    public void AddBook(Book book)
    {
        books.Add(book);
    }
}