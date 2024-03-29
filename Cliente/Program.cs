using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.UseElectron(args);
builder.Services.AddElectron();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


await app.StartAsync();

// Configuracion de la aplicacion a realizarse
BrowserWindowOptions options = new BrowserWindowOptions();

bool enableDevTools = false;
options.AutoHideMenuBar = true;

Console.WriteLine("HOOOOOLA");
Console.WriteLine(app.Environment.EnvironmentName);
if (app.Environment.IsDevelopment())
{
    enableDevTools = true;
    options.AutoHideMenuBar = false;
}

options.WebPreferences = new WebPreferences() {
    NodeIntegration = false,
    DevTools = enableDevTools
};

await Electron.WindowManager.CreateWindowAsync(options);
app.WaitForShutdown();

//app.Run();
