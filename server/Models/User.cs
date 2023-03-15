using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic;
using System.Runtime.CompilerServices;

namespace server.Models
{   
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public File? File { get; set; }
    }
}
