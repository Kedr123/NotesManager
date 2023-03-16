using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestUpdateList
    {
        [Required]
        public long Id { get; set; }

        public string? Title { get; set; }

        public IFormFile? Image { get; set; }
    }
}
