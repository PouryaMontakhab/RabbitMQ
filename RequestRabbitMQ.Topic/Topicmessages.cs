using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestRabbitMQ.Topic
{
    public class Topicmessages
    {
        private const string UName = "guest";
        private const string PWD = "guest";
        private const string HName = "localhost";

        public void SendMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = UName,
                Password = PWD,
                HostName = HName
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();



            //1.Crate Exchange
            model.ExchangeDeclare("topic.exchange", ExchangeType.Topic);
            Console.WriteLine("Creating Exchange");

            //2.Crate Queue
            model.QueueDeclare("demoqueueTopic", true, false, false, null);
            Console.WriteLine("Creating Queue");

            //3.Bind created queue to existing exchange
            model.QueueBind("demoqueueTopic", "topic.exchange", "Message.Bombay.Email");
            Console.WriteLine("Creating Binding");

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            byte[] messagebuffer = Encoding.Default.GetBytes("Message from Topic Exchange 'Bombay' ");
            model.BasicPublish("topic.exchange", "Message.Bombay.Email", properties, messagebuffer);
            Console.WriteLine("Message Sent From: topic.exchange ");
            Console.WriteLine("Routing Key: Message.Bombay.Email");
            Console.WriteLine("Message Sent");
        }
    }
}
