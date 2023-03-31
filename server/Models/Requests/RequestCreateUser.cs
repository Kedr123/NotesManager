using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestCreateUser : User
    {
        
        [Required]
        [MaxLength(40, ErrorMessage = "{0} не может превышать {1} символов")]
        [MinLength(6, ErrorMessage = "{0} должен быть минимум {1} символов")]
        new public string? Password { get; set; }

        //[FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        new public IFormFile? File { get; set; }
    }
}
