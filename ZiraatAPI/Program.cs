Environment.SetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "1");

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

// Swagger arayüzü her ortamda (Production dahil) aktif edilir
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ziraat API v1");
    c.RoutePrefix = "swagger"; // Adres: /swagger
});

app.UseCors();

// Kök dizine (/) gelen istekleri doğrudan Swagger sayfasına yönlendiriyoruz
app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

app.MapControllers();
app.Run();