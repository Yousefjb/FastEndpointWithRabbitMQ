global using FastEndpoints;
using CatalogService.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<CatalogContext>();
builder.Services.AddSingleton<CatalogMessageQueue>();

var app = builder.Build();
//app.UseAuthorization();
app.UseFastEndpoints();
app.Run();