using Microsoft.AspNetCore.Components.Web;

namespace simpleReading;

public  class Book
{
    public  Book()
    { }
    public Book(string title, string author, bool readed)
    {
        Title = title;
        Author = author;
        Readed = readed;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool Readed { get; set; }
}
