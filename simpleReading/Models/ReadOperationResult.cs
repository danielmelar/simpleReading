namespace simpleReading.Models
{
    public class ReadOperationResult
    {
        public ReadOperationResult(bool sucess, string message, Read read)
        {
            Sucess = sucess;
            Message = message;
            Read = read;
        }
        public ReadOperationResult(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public Read? Read { get; set; }
    }
}
