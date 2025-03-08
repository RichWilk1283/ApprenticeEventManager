using ApprenticeEventManager.Components;
using ApprenticeEventManager.DatabaseServices;
using ApprenticeEventManager.LoginServices;

var builder = WebApplication.CreateBuilder(args);

//Added for using in a docker container.
// Configure Kestrel to listen on all network interfaces
builder.WebHost.ConfigureKestrel(serverOptions =>
{
  serverOptions.ListenAnyIP(8080); // Match the Docker port
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<DbService>();
builder.Services.AddSingleton<LoginService>();

var app = builder.Build();

var dataBase = app.Services.GetRequiredService<DbService>();
dataBase.InitialiseDb();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
