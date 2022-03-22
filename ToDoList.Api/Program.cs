using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioConnection"),
                assembly => assembly.MigrationsAssembly(
                    typeof(PortfolioContext).Assembly.FullName))
                );

builder.Services.AddScoped<IToDoDataAccessService, ToDoDataAccessService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var InitialisationService = services.GetRequiredService<IToDoDataAccessService>();

    DbInitialiser.Initialise(InitialisationService);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
