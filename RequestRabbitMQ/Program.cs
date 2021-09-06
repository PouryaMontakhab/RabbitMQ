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
            //model.ExchangeDeclare("demoExchange", ExchangeType.Direct);
            //Console.WriteLine("Creating Exchange");

            //model.QueueDeclare("demoqueue", true,false,false,null);
            //Console.WriteLine("Creating Queue");

            //model.QueueBind("demoqueue", "demoExchange", "dirextexchange_key");
            //Console.WriteLine("Creating Binding");

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messageBuffer = Encoding.Default.GetBytes("Direct message ...");


            model.BasicPublish("demoExchange", "dirextexchange_key", properties, messageBuffer);
            Console.WriteLine("Message sent");

            Console.ReadLine();

        }
    }
}
