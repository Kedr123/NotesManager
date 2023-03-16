using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestCreateColumn
    {
        [Required]
        public long ListId { get; set; }

        [Required]
        public string? Title { get; set; }
    }
}
