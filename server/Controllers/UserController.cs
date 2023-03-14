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
        public async Task<User?> Registration(User user)
        {


            if (user == null)
            {
                return null;
            }

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();


            return user;
        }
    }
}
