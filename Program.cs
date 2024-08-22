using Barbershop.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BarbershopContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapControllers();

app.Run();
