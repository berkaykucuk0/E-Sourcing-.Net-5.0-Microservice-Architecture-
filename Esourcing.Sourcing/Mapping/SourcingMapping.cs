using AutoMapper;
using Esourcing.Sourcing.Entities;
using EventBusRabbitMQ.Events.Concrede;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Mapping
{
    public class SourcingMapping:Profile
    {
        public SourcingMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
