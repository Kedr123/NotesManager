using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Helpers;
using server.Models;
using server.Models.Requests;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly ApplicationDbContext dbContext;

        public readonly IWebHostEnvironment webHostEnvironment;

        public UserController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) 
            => (this.dbContext, this.webHostEnvironment) = (dbContext, webHostEnvironment);

        [HttpPost]
        public async Task<ActionResult<User>> Registration([FromForm]RequestCreateUser request)
        {
            var user = new User();
            user.Email = request.Email;
            user.Password = request.Password;

            bool isFile = false;

            if (request.File != null)
            {
                var allowedType = new[] { "image/jpg", "image/png", "image/gif", "image/jpeg", "image/bmp", "image/svg+xml" };

                if (!allowedType.Contains(request.File.ContentType)) 
                    return BadRequest(new {errors = "The File field only accepts files with the following extensions: .jpg, .png, .gif, .jpeg, .bmp, .svg" });
                
                var file = await FileHelper.FileUpload(dbContext, webHostEnvironment, request.File);
                user.File = file;
                isFile = true;
            }

            try
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (isFile)
                {
                    FileHelper.FileDelete(dbContext, webHostEnvironment, user.File.FileName);
                    dbContext.Files.Remove(user.File);
                    await dbContext.SaveChangesAsync();
                }

                 return BadRequest(new {errors = new {Email = new string[1] { "Not unique" } }});
            }
            
            


            return Ok(new { user });
        }
    }
}
