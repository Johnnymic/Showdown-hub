using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Showdown_hub.Api.Extension;
using Showdown_hub.Api.Services.Implementation;
using Showdown_hub.Api.Services.Interface;
using Showdown_hub.Data.DbContext;
using Showdown_hub.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurationService(builder.Configuration);

builder.Services.ConfigureDb(builder.Configuration);





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
