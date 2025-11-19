using HeyDEAN_API.Data;
using HeyDEAN_API.Repositories;
using HeyDEAN_API.Repositories.Interfaces;
using HeyDEAN_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Gets ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Adds DbContext, using MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add Repos and Services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IIntentService, IntentService>();
builder.Services.AddScoped<IVoiceService, VoiceService>();
builder.Services.AddTransient<Seeder>();

var app = builder.Build();

// Sætter scope på hvilken database og migrationer der skal bruges
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    // Tilføjer data fra .CSV ind i DB
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    seeder.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
