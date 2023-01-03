using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Domain.Repositories.Implementations;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using Model.Entities;
using MudBlazor;
using MudBlazor.Services;
using View.Components.Game.Country;
using View.Components.Game.Unit;
using BlazorPanzoom;
using Domain.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using View.Components.Game.Channel;
using View.Services;
using Microsoft.AspNetCore.Identity;

//setup firewall
IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 26280);

TcpListener t = new TcpListener(ipLocalEndPoint);
t.Start();
t.Stop();


//start docker container
var process = new Process();
var startInfo = new ProcessStartInfo{
    WindowStyle = ProcessWindowStyle.Hidden,
    FileName = "cmd.exe"
};
process.StartInfo = startInfo;
process.StartInfo.Arguments = "/c cd ..\\Databases && docker-compose down && docker-compose up -d --build";
process.Start();
process.WaitForExit();

//configure builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GreatPowersDbContext>(
    options => {
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 27)),
            retry => retry.EnableRetryOnFailure()
        );
        options.UseLoggerFactory(new NullLoggerFactory());
    });

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<GreatPowersDbContext>();

//Configure MudBlazor Snackbar
builder.Services.AddMudServices(config => {
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorPanzoomServices();
//Register repositories
builder.Services.AddScoped<AircraftCarrierRepository>();
builder.Services.AddScoped<FactoryRepository>();
builder.Services.AddScoped<LandRegionRepository>();
builder.Services.AddScoped<LandUnitRepository>();
builder.Services.AddScoped<NationRepository>();
builder.Services.AddScoped<NeighbourRepository>();
builder.Services.AddScoped<PlaneRepository>();
builder.Services.AddScoped<ShipRepository>();
builder.Services.AddScoped<TransportRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<WaterRegionRepository>();
builder.Services.AddScoped<SessionInfoRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddLogging();

builder.Services.AddScoped<SidebarService>();
builder.Services.AddScoped<DockerService>();

builder.Services.AddScoped<CountryPaths>();
builder.Services.AddScoped<UnitPaths>();
builder.Services.AddScoped<ChannelPaths>();

builder.Services.AddScoped<ActiveRegion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()){
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();