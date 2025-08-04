using BookStoreManager.Controllers;
using Serilog;
using Serilog.Events;

try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/logs_txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
        .WriteTo.File("logs/errors_txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    );

    builder.Logging.ClearProviders();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();


    app.Run();
}
catch(Exception ex)
{
    Log.Error(ex.Message);
}