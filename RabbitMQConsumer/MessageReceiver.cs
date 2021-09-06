using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer
{
    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel _channel;
        public MessageReceiver(IModel channel)
        {
            _channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine("Consuming message.");
            Console.WriteLine($"Message received from exchange : {exchange}");
            Console.WriteLine($"Consumer tag : {consumerTag}");
            Console.WriteLine($"Delivery tag : {deliveryTag}");
            Console.WriteLine($"Routing tag : {routingKey}");
            Console.WriteLine($"Message : {Encoding.UTF8.GetString(body.ToArray())}");

            _channel.BasicAck(deliveryTag, false);
        }

    }
}
