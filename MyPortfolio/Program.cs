using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UI.UIServices;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<UIToDoListService>();
builder.Services.AddHttpClient<IUIToDoListService, UIToDoListService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7127/");// only this one
    //client.DefaultRequestHeaders.Accept.Clear();
    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
