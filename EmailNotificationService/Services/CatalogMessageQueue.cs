using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace EmailNotificationService.Services
{
  public class CatalogMessageQueue
  {
    ConnectionFactory _factory;
    IConnection _conn;
    IModel _channel;

    public CatalogMessageQueue()
    {
      // TODO: Refactor username and password to use environment variables
      _factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
      _factory.UserName = "guest";
      _factory.Password = "guest";
      _conn = _factory.CreateConnection();
      _channel = _conn.CreateModel();
      _channel.QueueDeclare(queue: CatalogMessageType.ProductCreated,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);


      var consumer = new EventingBasicConsumer(_channel);
      consumer.Received += (model, ea) =>
      {
        var body = ea.Body.Span;
        var product = Encoding.UTF8.GetString(body);

        Console.WriteLine("I shuld send an email somewhere that this new product is created", product);
      };
      _channel.BasicConsume(queue: CatalogMessageType.ProductCreated,
                              autoAck: true,
                              consumer: consumer);
    }
  }

  //TODO: Move this to a shared library
  public static class CatalogMessageType
  {
    public const string ProductCreated = "ProductCreated";
  }
}