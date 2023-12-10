// See https://aka.ms/new-console-template for more information

using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

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

var a = 1;
while (a < 10)
{
    var obj1 = new { key = 1, value = "Do Chi Hung " + a };
    var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj1));
    
    channel.BasicPublish(
        exchange: "do-chi-hung-exchange",
        routingKey: "do.chi.hung",
        basicProperties: null,
        messageBody);
    
    Console.WriteLine(obj1.value);
    
    
    var obj2 = new { key = 2, value = "Do Chi Hoa " + a };
    messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj2));
    
    channel.BasicPublish(
        exchange: "do-chi-hung-exchange",
        routingKey: "do.chi.hoa",
        basicProperties: null,
        messageBody);
    
    
    a++;

    await Task.Delay(3000);

}

Console.ReadKey();

channel.Close();
connection.Close();