using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace simpleReading.Models
{
    public class Read
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Um título é necessário")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Um autor é necessário")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Fonte")]
        public string?  Source{ get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data da leitura")]
        [Required(ErrorMessage = "Uma data é necessária")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Leitura finalizada")]
        public bool Finished { get; set; } = true;

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
