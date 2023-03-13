using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class NoteFile
    {
        public long Id { get; set; }

        [Required]
        public Note? Note { get; set; }

        [Required]
        public File? File { get; set; }
    }
}
