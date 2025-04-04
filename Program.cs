using ApprenticeEventManager.Components;
using ApprenticeEventManager.DatabaseServices;
using ApprenticeEventManager.LoginServices;
using Microsoft.AspNetCore.Authentication.Cookies;

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.Cookie.Name = "aem_authtoken";
    options.LoginPath = "/login";
    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
  });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddSingleton<DbService>();
builder.Services.AddSingleton<TeamDb>();
builder.Services.AddSingleton<UserDb>();
builder.Services.AddSingleton<RoleDb>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
