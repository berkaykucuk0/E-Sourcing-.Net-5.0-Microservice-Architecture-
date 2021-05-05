﻿using Esourcing.Sourcing.Data.Abstract;
using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Data.Concrede
{
    public class SourcingContext:ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));
            SourcingDataSeed.SeedData(Auctions);
        }

        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
