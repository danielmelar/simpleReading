namespace simpleReading.Service;

public class BookService
{
    private int Id = 0;
    public List<Book> books = new List<Book>();

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

    public Book GetBookById(int id)
    {
        return books.First(x => x.Id == id);
    }

    public void UpdateRead(Book book, int id)
    {
        var tempRead = books.First(x => x.Id == id);
        RemoveBook(tempRead.Id);

        tempRead.Id = id;
        tempRead.Author = book.Author;
        tempRead.Readed = book.Readed;
        tempRead.Title = book.Title;

        AddBook(tempRead);
    }
}