using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Core
{
    //Publish işleminde kullanılacak queueName'leri vermek için bir class oluşturduk
    public static class EventBusConstants
    {
        public const string OrderCreateQueue = "orderCreateQueue";
    }
}
