namespace simpleReading.Models
{
    public class ReadOperationResult
    {
        public ReadOperationResult(bool sucess, string message, Read user)
        {
            Sucess = sucess;
            Message = message;
            User = user;
        }
        public ReadOperationResult(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public Read? User { get; set; }
    }
}
