using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<PortfolioContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioConnection"),
                assembly => assembly.MigrationsAssembly(
                    typeof(PortfolioContext).Assembly.FullName))
                );

builder.Services.AddScoped<IListService, ListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    var context = services.GetRequiredService<PortfolioContext>();
//    //context.Database.EnsureCreated();
//    //DbInitialiser.Initialise(context);
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=ToDoList}/{action=GetLists}/{id?}");
});

app.MapControllers();
app.MapRazorPages();

app.Run();
