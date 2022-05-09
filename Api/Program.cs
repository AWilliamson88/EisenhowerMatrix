using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioConnection"),
                assembly => assembly.MigrationsAssembly(
                    typeof(PortfolioContext).Assembly.FullName))
                );

builder.Services.AddScoped<IToDoDataService, ToDoDataService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var InitialisationService = services.GetRequiredService<IToDoDataService>();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
