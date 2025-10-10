using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlexAdmin.Components;
using PlexAdmin.Components.Account;
using PlexAdmin.Data;
using PlexAdmin.Infrastructure;
using PlexAdmin.Services;
using System.Net.Http.Headers;
using BlazorDownloadFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Blazor Server circuit options
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
        options.DisconnectedCircuitMaxRetained = 100;
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    });

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Configure Plex API
var plexToken = Environment.GetEnvironmentVariable("PLEX_TOKEN")
    ?? builder.Configuration["Plex:Token"]
    ?? throw new InvalidOperationException("Plex token not configured. Set PLEX_TOKEN environment variable or Plex:Token in appsettings.json");

var plexUrl = Environment.GetEnvironmentVariable("PLEX_URL")
    ?? builder.Configuration["Plex:Url"]
    ?? "http://localhost:32400";

// Parse Plex URL to extract components
var uri = new Uri(plexUrl);

// Set up HttpClient for Plex API, see https://github.com/LukeHagar/plexcsharp/issues/10 
var client = new PlexHTTPClient(uri, plexToken);

builder.Services.AddSingleton<IPlexAPI>(sp => new PlexAPI(
    client: client
));

builder.Services.AddScoped<IPlexService, PlexService>();

// Add BlazorDownloadFile service
builder.Services.AddBlazorDownloadFile();

var app = builder.Build();

// Add global exception handling for diagnostics
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Request started: {Method} {Path}", context.Request.Method, context.Request.Path);

    try
    {
        await next();
        logger.LogInformation("Request completed: {Method} {Path} - Status: {StatusCode}",
            context.Request.Method, context.Request.Path, context.Response.StatusCode);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Unhandled exception in request pipeline for {Path}", context.Request.Path);
        throw; // Re-throw to let default handler deal with it
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
