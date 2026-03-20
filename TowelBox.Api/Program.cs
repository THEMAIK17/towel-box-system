using Microsoft.EntityFrameworkCore;
using TowelBox.Application.Interfaces;
using TowelBox.Infrastructure.Data;
using TowelBox.Infrastructure.Repositories;
using TowelBox.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

//database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

// repositories
builder.Services.AddScoped<ITowelRepository, TowelRepository>();
builder.Services.AddScoped<IBoxRepository, BoxRepository>();

//services
builder.Services.AddScoped<ITowelService, TowelService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddControllers();
// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();