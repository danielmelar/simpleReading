using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace simpleReading.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Read> Reads { get; set; }
    }
}
