using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestUpdateColumn
    {
        [Required]
        public long Id { get; set; }

        public string? Title { get; set; }

        public long? ListId { get; set; }
    }
}
