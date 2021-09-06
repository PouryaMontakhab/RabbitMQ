using RabbitMQ.Client;
using System;

namespace RabbitMQConsumer
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
            var channel = connection.CreateModel();

            /*Here, prefetchCount is used to tell RabbitMQ not to give more than one message at a time to worker. Or,
            in other words, don't dispatch a new message to a worker until it has processed and acknowledged the previous one.
            Instead, it will dispatch to the next worker that is not still busy.*/
            channel.BasicQos(0, 1, false);


            
            var messageReveiver = new MessageReceiver(channel);
            channel.BasicConsume("demoqueue", false,messageReveiver);

            Console.ReadLine();
        }
    }
}
