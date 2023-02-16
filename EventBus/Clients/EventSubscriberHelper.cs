using EventBus.Events;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace EventBus.Clients;

public class EventSubscriberHelper{
    
    private readonly IConfiguration _configuration;
    public readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    public IModel _channel;
    public string _queueName;
    private string _exchangeName;
    private string _uniqueId = new (Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10).Select(s => s[Random.Next(s.Length)]).ToArray());
    private static readonly Random Random = new ();

    public EventSubscriberHelper(IConfiguration configuration, IEventProcessor processor) {
        _configuration = configuration;
        _eventProcessor = processor;
        Init();
    }

    public void Init() {
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
        _queueName = $"{_configuration["EventBusQueue"]}{_uniqueId}";
        _exchangeName = _configuration["EventBusExchange"];
        
        _channel.QueueDeclare(_queueName,true,false,false);
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
        _channel.QueueBind(_queueName,_exchangeName,"");
    }
}