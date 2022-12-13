using RabbitMQ.Client;

namespace ItemService.RabbitMqClient;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly string _nomeFila;
    private readonly IConnection _connection;
    private readonly IModel _channel;

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
        throw new NotImplementedException();
    }
}