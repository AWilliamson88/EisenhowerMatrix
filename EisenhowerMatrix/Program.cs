using EisenhowerMatrix.UIServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<UIToDoListService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<DefaultDataService>();
builder.Services.AddHttpClient<IUIToDoListService, UIToDoListService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7127/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
