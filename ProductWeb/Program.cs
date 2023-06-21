using Microsoft.EntityFrameworkCore;
using ProductWeb.Model;
using JwtAuthenticationManger;

var builder = WebApplication.CreateBuilder(args);

var dbhost = Environment.GetEnvironmentVariable("DB_HOST");
var dbname = Environment.GetEnvironmentVariable("DB_NAME");
var dbpassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

var connectionstring = $"server={dbhost},port=3306;database={dbname};user=root;password={dbpassword}";
builder.Services.AddDbContext<ProductContext>(o => o.UseMySQL(connectionstring));
builder.Services.AddCustomJwtAuthExtension();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
