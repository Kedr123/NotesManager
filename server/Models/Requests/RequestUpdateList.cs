using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestUpdateList
    {
        [Required]
        public long Id { get; set; }

        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string? Title { get; set; }

        public IFormFile? Image { get; set; }
    }
}
