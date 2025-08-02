using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using UserManagement.Data;
using UserManagement.Stores;

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
    builder.Services.AddSession();
    builder.Services.AddScoped<UsersStore>();

    builder.Services.AddDbContext<UserManagementDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "DataProtectionKeys")))
        .SetApplicationName("UserManagement");

    builder.Services.AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
        {
            options.LoginPath = "/Auth/Login";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });

    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXhcd3RXRWVYVk1/V0VWYEk=");

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

    app.UseSession();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Register}/{id?}")
        .WithStaticAssets();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
        dbContext.Database.Migrate();
    }


    app.Run();
}
catch(Exception ex)
{
    Log.Error(ex.Message);
}
