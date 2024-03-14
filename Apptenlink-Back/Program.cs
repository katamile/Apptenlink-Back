using Apptenlink_Back.Entities;
using Apptenlink_Back.Repositories.ClienteRepository;
using Apptenlink_Back.Repositories.GenericRepository;
using Apptenlink_Back.Services;
using Apptenlink_Back.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Obtener la cadena de conexión desde la variable de entorno
var connectionString = Environment.GetEnvironmentVariable(StringHandler.Database);

// Agregar el contexto de la base de datos
builder.Services.AddDbContext<DbContextApptelink>(options =>
    options.UseSqlServer(connectionString)
);


builder.Services.AddScoped<IClienteRepository, ClienteRepository>(); 
builder.Services.AddScoped<IClienteService, ClienteService>(); 

// Agregar Swagger/OpenAPI
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
