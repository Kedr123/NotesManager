using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace server.Models
{
    public class File
    {
        public long Id {  get; set; }

        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? OldFileName { get; set; }
    }
}
