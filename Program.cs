using Microsoft.EntityFrameworkCore;
using Payments.DB;
using Payments.Helpers;

// Construye la aplicación web
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios esenciales para la API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Agrega soporte para Swagger UI

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Connection"));
}, ServiceLifetime.Singleton);
builder.Services.AddScoped<Payments.Services.PaymentProcessorService>();
builder.Services.AddScoped<Payments.Services.AuthorizationService>();

// Construye la aplicación
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
    // Crea los datos necesarios para ejecutar los procesos
    DatabaseHelper databaseHelper = new DatabaseHelper(context);
    databaseHelper.SetUpDatabase();
}

// Configura el pipeline de solicitudes HTTP
app.UseSwagger();  // Habilita Swagger UI
app.UseSwaggerUI();  // Agrega la interfaz de Swagger UI
app.UseHttpsRedirection();  // Redirigir a HTTPS si es necesario
app.UseAuthorization();  // Habilita el middleware de autorización
app.MapControllers();  // Mapea los controladores de la API

// Inicia la aplicación
app.Run();