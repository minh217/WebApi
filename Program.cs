using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Repositories;
using WebApi.Helpers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddCors();
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseCors(options => options
                        .WithOrigins(new []{
                                                "http://localhost:3000",
                                                "http://localhost:8080",
                                                "http://localhost:4200"
                                            })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
app.UseAuthorization();

app.MapControllers();

app.Run();
