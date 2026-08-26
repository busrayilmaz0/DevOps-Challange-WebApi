using Microsoft.EntityFrameworkCore;
using ZiraatApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Render / Docker port yapılandırması
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// Swagger servisleri ekleniyor
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Swagger arayüzünü aktifleştiriyoruz
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ziraat API v1");
    c.RoutePrefix = "swagger"; // Adres: http://localhost:5000/swagger
});

app.UseCors();

app.MapGet("/", () => "Hello Ziraat Team");
app.MapControllers();

app.Run();