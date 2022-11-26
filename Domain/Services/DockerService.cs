using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Model.Configuration;
using MySqlConnector;

namespace Domain.Services;

public class DockerService{

    private readonly GreatPowersDbContext _greatPowersDbContext;

    public DockerService(GreatPowersDbContext greatPowersDbContext){
        _greatPowersDbContext = greatPowersDbContext;
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
    
    public Task ChangeDbContext(string ipAddress, int port){
        try{
            AppSettings.Port = $"{port}";
            _greatPowersDbContext.Database.CloseConnection();
            _greatPowersDbContext.Database.SetConnectionString($"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
            _greatPowersDbContext.Database.OpenConnection();
            return Task.CompletedTask;
        }
        catch (Exception){
            return Task.CompletedTask;
        }
    }
    
    public bool CheckConnection(string ipAddress, int port){
        try{
            var connection = new MySqlConnection($"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
            connection.Open();
            connection.Close();
            return true;
        }
        catch (Exception){
            return false;
        }
    }
}