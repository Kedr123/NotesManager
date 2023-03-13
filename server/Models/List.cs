using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class List
    {
        public long Id { get; set; }

        [Required]
        public string? Title { get; set; }

        
        public File? File { get; set; }

        [Required]
        public User? User { get; set; }
    }
}
