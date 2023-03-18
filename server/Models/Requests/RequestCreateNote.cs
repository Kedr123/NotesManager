using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestCreateNote
    {
        [Required]
        public long ColumnId { get; set; }

        [Required]
        public string? Text { get; set; }

        public IFormFileCollection? files { get; set; }
    }
}
