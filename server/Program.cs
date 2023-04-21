using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server;
using server.Interfaces;
using server.Models;
using server.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(options => {
    //options.AddPolicy("AllPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("AllPolicy", opt => opt.WithOrigins("https://192.168.3.179:8080", "https://127.0.0.1:8080", "https://localhost:8080").AllowCredentials().AllowAnyHeader().AllowAnyMethod());
    //options.AddPolicy("AllPolicy2", opt => opt.AllowCredentials());
});
    


var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true

        };
    });

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultLink")));

builder.Services.AddScoped<IRepositoryList, RepositoryList>();
builder.Services.AddScoped<IRepositoryColumn, RepositoryColumn>();
builder.Services.AddScoped<IRepositoryNote, RepositoryNote>();





var app = builder.Build();

app.Use(async (ctx, next) =>
{
    if (ctx.Request.Method.Equals("options", StringComparison.InvariantCultureIgnoreCase) && ctx.Request.Headers.ContainsKey("Access-Control-Request-Private-Network"))
    {
        ctx.Response.Headers.Add("Access-Control-Allow-Private-Network", "true");
    }

    await next();
});
app.UseCors("AllPolicy");
//app.UseCors("AllPolicy2");



app.UseHttpsRedirection();

app.UseStaticFiles();
/*app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});*/

app.UseCookiePolicy();

// Подмена токина из запроса на токин из куки 
app.Use((context, next) =>
{

    if(context.Request.Path != "/api/auth/refresh")
    {
        return next();
    }

    context.Request.Headers.Remove("Authorization");

    var cookie = context.Request.Cookies["RefreshToken"];

    Console.WriteLine(cookie);

    if (cookie != null)
    {
        var token = cookie;
        context.Request.Headers.Append("Authorization", "Bearer " + token);
    }

    return next();


});



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseCors("AllPolicy");

app.Run();
