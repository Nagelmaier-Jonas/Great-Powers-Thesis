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
    
    private readonly EventSubscriberHelper _eventSubscriberHelper; 

    public EventSubscriber(EventSubscriberHelper eventSubscriberHelper) {
        _eventSubscriberHelper = eventSubscriberHelper;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        stoppingToken.ThrowIfCancellationRequested();
        
        var consumer = new EventingBasicConsumer(_eventSubscriberHelper._channel);
        
        consumer.Received += (moduleHandle, message) => {
            var body = message.Body;
            var eventMessage = Encoding.UTF8.GetString(body.ToArray());
            
            _eventSubscriberHelper._eventProcessor.ProcessEvent(eventMessage);
        };

        _eventSubscriberHelper._channel.BasicConsume(_eventSubscriberHelper._queueName, true, consumer);
        
        return Task.CompletedTask;
    }
}