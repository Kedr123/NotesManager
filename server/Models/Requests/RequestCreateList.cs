using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{   
    
    public class RequestCreateList
    {        
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string? Title { get; set; }



        [FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
        public IFormFile? Image { get; set; }
    }
}
