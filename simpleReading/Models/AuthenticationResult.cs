namespace simpleReading.Models
{
    public class AuthenticationResult
    {
        public AuthenticationResult(bool sucess, string message, User user)
        {
            Sucess = sucess;
            Message = message;
            User = user;
        }
        public AuthenticationResult(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public User? User { get; set; }
    }
}
