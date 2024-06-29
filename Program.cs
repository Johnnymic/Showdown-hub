using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showdown_hub.Api.Extension;
using Showdown_hub.Api.Services.Implementation;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Data.DbContext;
using Showdown_hub.Data.Reposiotry.Implementation;
using Showdown_hub.Data.Reposiotry.Interface;
using Showdown_hub.Models;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.ConfigurationService(builder.Configuration);

//builder.Services.ConfigureDb(builder.Configuration);
 //builder.Services.AddIdentity<ApplicationUser, IdentityRole>() .AddEntityFrameworkStores<EventHubContext>().AddDefaultTokenProviders();
 builder.Services.AddDbContext<EventHubContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("EventHubDb")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<EventHubContext>()
    .AddDefaultTokenProviders();


 builder.Services.AddScoped<IAccountRepo, AccountRepo>();
 builder.Services.AddScoped<IAccountService, AccountService> ();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Username settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true; // Ensure emails are unique

    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Sign in settings
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

 builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(IAccountService).Assembly);

//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly, typeof(IAccountService).Assembly, typeof(AccountService).Assembly);

//For authentication 

builder.Services.AddAuthentication(options =>
{
       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options=>
{
       options.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateAudience = true,
           ValidateIssuer = true,
           ValidateLifetime =true,  
           ValidateIssuerSigningKey = true,
           ValidAudience = builder.Configuration["JWT:ValidAudience"],
           ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
           ClockSkew = TimeSpan.FromMinutes(30)


       };


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
