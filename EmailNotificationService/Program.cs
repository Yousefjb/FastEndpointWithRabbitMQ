global using FastEndpoints;
using EmailNotificationService.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<CatalogMessageQueue>(new CatalogMessageQueue());

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.Run();