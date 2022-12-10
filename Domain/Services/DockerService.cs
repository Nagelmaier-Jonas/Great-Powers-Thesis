using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Configuration;
using MySqlConnector;

namespace Domain.Services;

public class DockerService{

    private readonly GreatPowersDbContext _greatPowersDbContext;
    private ILogger<DockerService> Logger{ get; set; }

    public DockerService(GreatPowersDbContext greatPowersDbContext, ILogger<DockerService> logger){
        _greatPowersDbContext = greatPowersDbContext;
        Logger = logger;
    }

    public Task StartDockerContainer(string name){
        var process = new Process();
        var startInfo = new ProcessStartInfo{
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe"
        };
        process.StartInfo = startInfo;
        process.StartInfo.Arguments = $"/c cd ..\\Databases\\{name} && docker-compose up -d --build";
        process.Start();
        process.WaitForExit();
        return Task.CompletedTask;
    }
    
    public Task ChangeDbContext(string ipAddress, string port){
        try{
            Logger.Log(LogLevel.Warning, "Changing current Connection String: " + _greatPowersDbContext.Database.GetDbConnection().ConnectionString);
            AppSettings.Port = port;
            AppSettings.IpAddress = ipAddress;
            _greatPowersDbContext.Database.CloseConnection();
            _greatPowersDbContext.Database.SetConnectionString($"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
            _greatPowersDbContext.Database.OpenConnection();
            Logger.Log(LogLevel.Information, "New Connection String: " + _greatPowersDbContext.Database.GetDbConnection().ConnectionString);
            return Task.CompletedTask;
        }
        catch (Exception){
            Logger.Log(LogLevel.Error, "Cannot change connection");
            return Task.CompletedTask;
        }
    }
    
    public bool CheckConnection(string ipAddress, string port){
        Thread.Sleep(1000);
        try{
            var connection = new MySqlConnection($"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
            connection.Open();
            connection.Close();
            Logger.Log(LogLevel.Information, "Connection is valid");
            return true;
        }
        catch (Exception){
            Logger.Log(LogLevel.Warning, "Connection is not valid");
            return false;
        }
    }
}