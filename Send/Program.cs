//Send

using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue:"hello",
    durable:false,
    exclusive:false,
    autoDelete:false,
    arguments:null
);

const string message = "Hello World!";

var body = Encoding.UTF8.GetBytes(message);

for (int i = 0; i < 1000; i++)
{
    Thread.Sleep(1000);
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: "hello",
        basicProperties: null,
        body: body
    );

    Console.WriteLine($" [x] Sent {message}");
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();