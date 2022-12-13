using System.Text;
using ItemService.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ItemService.RabbitMqClient;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly string _nomeFila;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IProcessaEvento _processaEvento;

    public RabbitMqSubscriber(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 8002

        }.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        _nomeFila = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queue: _nomeFila, exchange: "trigger", routingKey: string.Empty);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (moduleHandle, e) => 
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            _processaEvento.Processar(message);
        };
    }
}