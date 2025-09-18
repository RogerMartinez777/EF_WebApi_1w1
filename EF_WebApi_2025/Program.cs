using EF_WebApi_2025.Data.Models;
using EF_WebApi_2025.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//--------------------------------------------------------------------
// Agregamos el builder librosDBContext para que podamos pasarlo por parametro en el constructor del Controller
builder.Services.AddDbContext<LibrosDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString
("DefaultConnection")));

// Cuando deba inyectar un libro repository hara un new repository
builder.Services.AddScoped<ILibroRepository, LibroRepository>();

//--------------------------------------------------------------------

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
