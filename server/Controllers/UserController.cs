using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public readonly ApplicationDbContext dbContext;

        public UserController(ApplicationDbContext dbContext) => (this.dbContext) = (dbContext);

        [HttpPost]
        public async Task<ActionResult<User>> Registration(User user)
        {


            if (user == null)
            {
                return  BadRequest();
            }

            try
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                 return BadRequest(new {errors = new {Email = new string[1] { "Not unique" } }});
            }
            
            


            return Ok(user);
        }
    }
}
