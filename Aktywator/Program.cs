


using Aktywator.DB;
using Aktywator.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:7268", "http://localhost:5285");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("CS"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CS"))));
builder.Services.AddScoped<IAktywatorService, AktywatorService>();
var app = builder.Build();

// Sprawdzenie œrodowiska
if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aktywator API v1");
        c.RoutePrefix = "swagger"; // Zmieniamy prefix na 'swagger'
    });
    }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();