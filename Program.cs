using Microsoft.EntityFrameworkCore;
using Payments.DB;
using Payments.Helpers;

// Build the web application
var builder = WebApplication.CreateBuilder(args);

// Adds essential services to the API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Adds Swagger UI support

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Connection"));
}, ServiceLifetime.Singleton);
builder.Services.AddScoped<Payments.Services.PaymentProcessorService>();
builder.Services.AddScoped<Payments.Services.AuthorizationService>();

// Build the application
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
    // Creates the data needed to run the processes
    DatabaseHelper databaseHelper = new DatabaseHelper(context);
    databaseHelper.SetUpDatabase();
}

// Configures the HTTP request pipeline
app.UseSwagger();  // Enable Swagger UI
app.UseSwaggerUI();  // Adds Swagger UI interface
app.UseHttpsRedirection();  // Redirect to HTTPS if necessary
app.UseAuthorization();  // Enables authorisation middleware
app.MapControllers();  // Map API controllers

// Start the application
app.Run();