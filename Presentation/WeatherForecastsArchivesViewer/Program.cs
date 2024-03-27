using WeatherForecastsArchivesViewer.Application;
using WeatherForecastsArchivesViewer.Application.Interfaces.Services;
using WeatherForecastsArchivesViewer.Infrastructure;
using WeatherForecastsArchivesViewer.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var logger = builder.Logging;

services.AddControllersWithViews();

services.AddSingleton<IExcelWeatherArchiveReader, ExcelWeatherArchiveReader>();
services.AddLogging();
services.AddApplication();
services.AddDatabase();

var app = builder.Build();

app.Services.ApplyMigrations(configuration, app.Logger);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
