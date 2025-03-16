namespace simpleReading;

public class ReadService
{
    public List<Read> reads = new List<Read>();

    public List<Read> GetReads() => reads; 

    public void AddRead(Read read)
    {
        read.Id = CreateId();
        reads.Add(read);
    } 

    private int CreateId()
    {
        return reads.Count() + 1;
    } 
}
