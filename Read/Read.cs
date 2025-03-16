namespace simpleReading;

public class Read
{
    public Read()
    { }

    public Read(string title, string author, bool readed, string source)
    {
        Title = title;
        Author = author;
        Readed = readed;
        Source = source;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool Readed { get; set; }
    public string Source { get; set; }
}   
