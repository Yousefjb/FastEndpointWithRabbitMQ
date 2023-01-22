using System.Text.Json;
using RabbitMQ.Client;

namespace CatalogService.Services
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
    }

    public void PublishProductCreated(Product product)
    {
      _channel.BasicPublish(exchange: "",
                              routingKey: CatalogMessageType.ProductCreated,
                              basicProperties: null,
                              body: JsonSerializer.SerializeToUtf8Bytes(product));
    }
  }

  public static class CatalogMessageType
  {
    public const string ProductCreated = "ProductCreated";
  }
}