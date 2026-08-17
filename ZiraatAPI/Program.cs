var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://localhost:11130");

var app = builder.Build();

app.MapGet("/", () => "Hello Ziraat Team");

app.Run();