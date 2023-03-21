using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly JWTSettings jwtS;
        public readonly ApplicationDbContext dbContext;

        public AuthController(IOptions<JWTSettings> jwtS, ApplicationDbContext dbContext)
            => (this.jwtS, this.dbContext) = (jwtS.Value, dbContext);




        [HttpPost]
        public  ActionResult PostAuth(string Email, string Password)
        {
            
            var user = dbContext.Users.Where(opt => opt.Email == Email && opt.Password == Password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }


            var jwtAccess = GenirateToken(15, user);



            var jwtRefresh = GenirateToken(24*60*15, user);

            
            var cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;

            try
            {
                Request.HttpContext.Response.Cookies.Append("RefreshToken", jwtRefresh, cookieOptions);
            }
            catch
            {

            }

            return Ok(new { jwtAccess, jwtRefresh });
        }

        [HttpPost("refresh"), Authorize]
        public ActionResult RefreshTokens()
        {
            var userId = HttpContext.User.FindFirst("Id")?.Value;

            if (userId == null) return NotFound();
            
            var user = dbContext.Users.FirstOrDefault(opt => opt.Id == Convert.ToInt64(userId));

            if (user == null) return NotFound();

            var jwtAccess = GenirateToken(15, user);

            var jwtRefresh = GenirateToken(24 * 60 * 15, user);

            var cookieOptions = new CookieOptions();
            cookieOptions.HttpOnly = true;

            try
            {
                Request.HttpContext.Response.Cookies.Append("RefreshToken", jwtRefresh, cookieOptions);
            }

            catch { }

            return Ok(new { jwtAccess, jwtRefresh });
        }

        [HttpGet]
        public void logout()
        {
            Request.HttpContext.Response.Cookies.Delete("RefreshToken");
            HttpContext.Response.Headers.Clear();
        }

        private string GenirateToken(int time, User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtS.SecretKey));


            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var jwt = new JwtSecurityToken(
                issuer: jwtS.Issuer,
                audience: jwtS.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(time)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
