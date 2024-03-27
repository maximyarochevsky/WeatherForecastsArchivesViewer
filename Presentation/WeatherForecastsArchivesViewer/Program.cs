using WeatherForecastsArchiveViewer.Application;
using WeatherForecastsArchiveViewer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();

services.AddLogging();
services.AddApplication();
services.AddDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
