using Microsoft.EntityFrameworkCore;
using ZiraatApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Render gibi platformların dinamik port atamalarını yakalaması için
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

// CORS Ayarları (Politikaya 'AllowAll' ismi vererek netleştiriyoruz)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ziraat API v1");
    c.RoutePrefix = "swagger";
});

// Tanımladığımız 'AllowAll' politikasını burada çağırıyoruz
app.UseCors("AllowAll");

app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

app.MapControllers();

app.Run();