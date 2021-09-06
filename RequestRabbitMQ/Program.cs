using RabbitMQ.Client;
using System;
using System.Text;

namespace RequestRabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName = "guest";
            string password = "guest";
            string hostName = "localhost";
            var connectionFactory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                HostName = hostName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            //1.Crate Exchange 
            //model.ExchangeDeclare("demoExchange", ExchangeType.Direct);
            //Console.WriteLine("Creating Exchange");

            //2.Crate Queue 
            //model.QueueDeclare("demoqueue", true,false,false,null);
            //Console.WriteLine("Creating Queue");

            //3.Bind created queue to existing exchange 
            //model.QueueBind("demoqueue", "demoExchange", "dirextexchange_key");
            //Console.WriteLine("Creating Binding");

            //4.Publish message
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            Console.WriteLine("Please enter your message here");
            var message = Console.ReadLine();
            byte[] messageBuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("demoExchange", "dirextexchange_key", properties, messageBuffer);
            Console.WriteLine("Message sent");

            Console.ReadLine();

        }
    }
}
