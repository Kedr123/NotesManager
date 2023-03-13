using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Column
    {
        public long Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public List? list { get; set; }

        
    }
}
