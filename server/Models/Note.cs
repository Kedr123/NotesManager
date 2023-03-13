using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Note
    {
        public long Id { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [Required]
        public Column? Column { get; set; }
    }
}
