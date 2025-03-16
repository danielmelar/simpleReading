namespace simpleReading;

public class ReadService
{
    public List<Read> reads = new List<Read>();

    public List<Read> GetReads() => reads; 

    public Read GetReadById(int id)
    {
        var foundRead = reads.First(x => x.Id == id);
        if (foundRead == null)
            return null;

        return foundRead;
    }

    public void AddRead(Read read)
    {
        read.Id = CreateId();
        reads.Add(read);
    } 

    public void RemoveRead(int id)
    {
        reads.Remove(reads.Find(x => x.Id == id));
    }

    private int CreateId()
    {
        return reads.Count() + 1;
    } 
}
