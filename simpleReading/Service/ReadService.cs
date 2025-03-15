namespace simpleReading;

public class ReadService
{
    public List<Read> reads = new List<Read>();

    public List<Read> GetReads() => reads; 

    public void AddRead(Read read)
    {
        reads.Add(read);
    }  
}
