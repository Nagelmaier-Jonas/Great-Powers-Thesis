using System.Diagnostics;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Configuration;
using MySqlConnector;

namespace Domain.Services;

public class DockerService{
    private readonly GreatPowersDbContext _greatPowersDbContext;
    private ILogger<DockerService> Logger{ get; set; }

    public List<int> PortsMysql{ get; set; } = new(){
        26280,
        26281,
        26282,
        26283,
        26284
    };
    
    public List<int> PortsRabbitMqS{ get; set; } = new(){
        26285,
        26286,
        26287,
        26288,
        26289
    };
    
    public List<int> PortsRabbitMqW{ get; set; } = new(){
        26290,
        26291,
        26292,
        26293,
        26294
    };

    public DockerService(GreatPowersDbContext greatPowersDbContext, ILogger<DockerService> logger){
        _greatPowersDbContext = greatPowersDbContext;
        Logger = logger;
    }

    public int GetPortMySql(){
        foreach (var p in PortsMysql.Where(IsPortFree)){
            return p;
        }

        throw new Exception("No Ports Available");
    }
    
    public int GetPortRabbitMqS(){
        foreach (var p in PortsRabbitMqS.Where(IsPortFree)){
            return p;
        }

        throw new Exception("No Ports Available");
    }
    
    public int GetPortRabbitMqW(){
        foreach (var p in PortsRabbitMqW.Where(IsPortFree)){
            return p;
        }

        throw new Exception("No Ports Available");
    }

    private bool IsPortFree(int port){
        try{
            using var tcpClient = new TcpClient("localhost", port);
            return false;
        }
        catch (Exception e){
            Logger.LogInformation(e.Message);
            return true;
        }
    }

    public Task WriteDockerFile(string name, string portmySql, string portRabbitMqS, string portRabbitMqW){
        var schema = File.ReadAllText("..\\Databases\\schema.sql");
        schema = schema.Replace("\\Databases\\default\\", $"\\Databases\\{name}\\");
        var dockerComposeFile = File.ReadAllText("..\\Databases\\docker-compose.yml");
        dockerComposeFile = dockerComposeFile.Replace("./default:/var/lib/mysql", $"./{name}:/var/lib/mysql");
        dockerComposeFile = dockerComposeFile.Replace("26280", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26285", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26290", portRabbitMqW);
        dockerComposeFile = dockerComposeFile.Replace("greatpowers_db", $"greatpowers_db_{name}");
        dockerComposeFile = dockerComposeFile.Replace("greatpowers_queue", $"greatpowers_queue_{name}");
        dockerComposeFile =
            dockerComposeFile.Replace("./default/rabbitmq:/var/lib/rabbitmq", $"./rabbitmq:/var/lib/rabbitmq");
        dockerComposeFile = dockerComposeFile.Replace("./default/mysql:/var/lib/mysql", $"./mysql:/var/lib/mysql");
        Directory.CreateDirectory($"..\\Databases\\{name}");
        File.WriteAllText($"..\\Databases\\{name}\\schema.sql", schema);
        File.WriteAllText($"..\\Databases\\{name}\\docker-compose.yml", dockerComposeFile);
        return Task.CompletedTask;
    }

    public Task StartDockerContainer(string name, string portmySql, string portRabbitMqS, string portRabbitMqW){
        var dockerComposeFile = File.ReadAllText("..\\Databases\\docker-compose.yml");
        dockerComposeFile = dockerComposeFile.Replace("26280", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26281", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26282", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26283", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26284", portmySql);
        dockerComposeFile = dockerComposeFile.Replace("26285", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26286", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26287", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26288", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26289", portRabbitMqS);
        dockerComposeFile = dockerComposeFile.Replace("26290", portRabbitMqW);
        dockerComposeFile = dockerComposeFile.Replace("26291", portRabbitMqW);
        dockerComposeFile = dockerComposeFile.Replace("26292", portRabbitMqW);
        dockerComposeFile = dockerComposeFile.Replace("26293", portRabbitMqW);
        dockerComposeFile = dockerComposeFile.Replace("26294", portRabbitMqW);
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

    public Task ChangeDbContext(string ipAddress, string port, string rabbitport, string rabbitport2){
        try{
            Logger.Log(LogLevel.Warning,
                "Changing current Connection String: " +
                _greatPowersDbContext.Database.GetDbConnection().ConnectionString);
            AppSettings.DBPort = port;
            AppSettings.RabbitPort = rabbitport;
            AppSettings.RabbitPort2 = rabbitport2;
            AppSettings.IpAddress = ipAddress;
            _greatPowersDbContext.Database.CloseConnection();
            _greatPowersDbContext.Database.SetConnectionString(
                $"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
            _greatPowersDbContext.Database.OpenConnection();
            Logger.Log(LogLevel.Information,
                "New Connection String: " + _greatPowersDbContext.Database.GetDbConnection().ConnectionString);
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
            var connection =
                new MySqlConnection(
                    $"server={ipAddress}; port={port}; database=greatpowers; user=greatpowers; password=greatpowers; Persist Security Info=False; Connect Timeout=300");
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

    public void StopDockerContainer(){
        var process = new Process();
        var startInfo = new ProcessStartInfo{
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe"
        };
        process.StartInfo = startInfo;
        process.StartInfo.Arguments = "/c cd ..\\Databases && docker-compose down";
        process.Start();
        process.WaitForExit();
    }
}