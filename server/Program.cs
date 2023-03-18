using Microsoft.AspNetCore.Authentication.JwtBearer;
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

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Use((context, next) =>
{

    if(context.Request.Path != "/api/auth/refresh")
    {
        return next();
    }

    context.Request.Headers.Remove("Authorization");

    var cookie = context.Request.Cookies["RefreshToken"];

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

app.Run();
