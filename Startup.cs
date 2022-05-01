using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using ElectronNET.API;
using ElectronNET.API.Entities;
using trackr.Data;
namespace trackr;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<trackrDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("Sqlite")));
        services.AddRazorPages().AddRazorRuntimeCompilation();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
        ElectronBootstrap();
    }

    public async void ElectronBootstrap()
    {
        BrowserWindowOptions options = new BrowserWindowOptions
        {
            Show = false,
            Resizable = false,
            Movable = true,
            Title = "trackr",
            Height = 1200,
            Width = 950,
            BackgroundColor = "#2F3136",
            DarkTheme = true,
            Icon = "/wwwroot/favicon.ico",
            AutoHideMenuBar = true,
            Frame = false,

            WebPreferences = new WebPreferences
            {
                ContextIsolation = true,
                DevTools = false,
                WebSecurity = false,
                EnableRemoteModule = true
            }

        };
        BrowserWindow mainWindow = await Electron.WindowManager.CreateWindowAsync(options);
        mainWindow.OnReadyToShow += () =>
        {
            mainWindow.Show();
        };
        mainWindow.RemoveMenu();
    }
}