using EventBusRabbitMQ.Events.Abstract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection;
        private readonly ILogger<EventBusRabbitMQProducer> _logger;
        private readonly int _retryCount;

        public EventBusRabbitMQProducer(IRabbitMQPersistentConnection rabbitMQPersistentConnection, ILogger<EventBusRabbitMQProducer> logger, int retryCount=5)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection;
            _logger = logger;
            _retryCount = retryCount;
        }
       
        //@ işaretiyle parametreyi yazmamızın sebebi event adında bişey tanımlı derleyicide
        public void Publish(string queueName,IEvent @event)
        {
            if (!_rabbitMQPersistentConnection.IsConnected)
            {
                _rabbitMQPersistentConnection.TryConnect();
            }

            //RabbitMQ'ya bağlanmak için sürekli istek yapıyoruz ve  _retryCount ile kaçıncı seferde bağlandığını logluyoruz ve matematiksel işlem yaparak kaç saniyede bağlandığını logluyoruz
            var policy = RetryPolicy.Handle<SocketException>()
               .Or<BrokerUnreachableException>()
               .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
               {
                   _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
               });

            //using Bir sınıf kullanıldıktan sonra, çöp toplayıcından (Garbage Collector) önce; IDisposable arayüzünün
            //(interface) Dispose metodunun çalıştırılarak hafızadan (memory) silinmesi işlemidir.
            using (var channel= _rabbitMQPersistentConnection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                //datayı jsona çevirip brokere publish edeceğiz bu yüzden jsonconvert kullandık
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                //rabbitmq default policy 
                policy.Execute(() =>
                {
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.DeliveryMode = 2;

                    channel.ConfirmSelect();
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queueName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                    channel.WaitForConfirmsOrDie();

                    channel.BasicAcks += (sender, eventArgs) =>
                    {
                        Console.WriteLine("Sent RabbitMQ");
                        //implement ack handle
                    };
                });
            }
        }
    }
}
