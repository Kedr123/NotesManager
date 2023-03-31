using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestCreateColumn
    {
        [Required]
        public long ListId { get; set; }

        [Required]
        [StringLength(maximumLength:25, MinimumLength = 1)]
        public string? Title { get; set; }
    }
}
