using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory();

factory.Port = 5672;
factory.Password = "guest";
factory.UserName = "guest";
factory.VirtualHost = "/";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

// channel.ExchangeDeclare("do-chi-hung-exchange", type: ExchangeType.Direct);
// channel.ExchangeDeclare("do-chi-hung-exchange", type: ExchangeType.Fanout);
channel.ExchangeDeclare("do-chi-hung-exchange", type: ExchangeType.Topic);
// channel.ExchangeDeclare("do-chi-hung-exchange", type: ExchangeType.Headers);
channel.QueueDeclare(queue: "high_priority_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: "high_priority_queue", exchange: "do-chi-hung-exchange", routingKey: "do.*.hung");

Console.WriteLine("Start Consumer Two");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var obj = JsonConvert.DeserializeObject<Obj>(message);

    if (obj.key == 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Message received: " + obj.value);
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine("Message received: " + obj.value);
    }
};

channel.BasicConsume(queue: "high_priority_queue", autoAck: true, consumer: consumer);

Console.WriteLine("Consumer is listening. Press any key to exit.");
Console.ReadKey();

channel.Close();
connection.Close();
public class Obj
{
    public int key { get; set; }
    public string value { get; set; }
}
