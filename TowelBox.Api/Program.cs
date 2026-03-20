using Microsoft.EntityFrameworkCore;
using TowelBox.Application.Interfaces;
using TowelBox.Infrastructure.Data;
using TowelBox.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

// repositories
builder.Services.AddScoped<ITowelRepository, TowelRepository>();
builder.Services.AddScoped<IBoxRepository, BoxRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();