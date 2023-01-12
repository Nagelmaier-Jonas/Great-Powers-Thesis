using System.Text;
using Domain.Services;
using EventBus.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus.Clients;

public class EventSubscriber : BackgroundService {
    
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public EventSubscriber(IConfiguration configuration, IEventProcessor processor) {
        _configuration = configuration;
        _eventProcessor = processor;
        Init();
    }

    private void Init() {
        var factory = new ConnectionFactory() {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]),
            UserName = "greatpowers",
            Password = "greatpowers",
            VirtualHost = "greatpowers"
        };
        while (true){
            try{
                _connection = factory.CreateConnection();
                break;
            }
            catch (Exception e){
                // ignored
            }
        }
        _channel = _connection.CreateModel();
        _queueName = _configuration["EventBusQueue"];
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();
        
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += (moduleHandle, message) => {
            var body = message.Body;
            var eventMessage = Encoding.UTF8.GetString(body.ToArray());
            
            _eventProcessor.ProcessEvent(eventMessage);
        };
        
        return Task.CompletedTask;
    }
}