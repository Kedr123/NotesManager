using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{   
    
    public class RequestCreateList
    {        
        [Required]
        public string? Title { get; set; }


        
        //[FileExtensions]
        public IFormFile? Image { get; set; }
    }
}
