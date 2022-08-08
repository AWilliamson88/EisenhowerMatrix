using Api;
using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EMDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EMConnection"),
                assembly => assembly.MigrationsAssembly(
                    typeof(EMDbContext).Assembly.FullName))
                );
builder.Services.AddScoped<IEMDataService, EMDataService>();
builder.Services.AddTransient<IDataSeeder, DataSeeder>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scopedFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopedFactory.CreateScope())
{
    var services = scope.ServiceProvider;

    var dataSeeder = services.GetRequiredService<IDataSeeder>();
    dataSeeder.Seed();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
