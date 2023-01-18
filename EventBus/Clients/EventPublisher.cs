using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace EventBus.Clients; 

public class EventPublisher : IEventPublisher {

    private readonly IConfiguration _configuration;
    
    private readonly string _exchangeName;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    
    public EventPublisher(IConfiguration configuration) {
        _configuration = configuration;
        
        var factory = new ConnectionFactory() {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
            Password = "greatpowers",
            UserName = "greatpowers",
            VirtualHost = "greatpowers"
        };

        try {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _connection.ConnectionShutdown += ShutDownMessageBroker;
            _exchangeName = _configuration["EventBusExchange"];
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    private void ShutDownMessageBroker(object? sender, ShutdownEventArgs e) => Console.WriteLine("Message broker connection shut down");


    public void Publish(string message) {
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(_exchangeName,  "", null, body);
    }
    
    public void Dispose() {
        if (_channel.IsOpen) {
            _channel.Close();
            _connection.Close();
        }
    }
}