using Backend_agendamientos.Core.Interfaces;
using Backend_agendamientos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Permitir cualquier origen
                  .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, etc.)
                  .AllowAnyHeader(); // Permitir cualquier cabecera
        });
});

// Configuración de la base de datos MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30)) // Cambia según tu versión de MySQL
    ));

// Registro de repositorios
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configuración de Swagger/OpenAPI
builder.Services.AddSwaggerGen(); // Asegúrate de que esto está antes de builder.Build()

// Configuración de servicios base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

// Habilitar CORS con la política definida
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
