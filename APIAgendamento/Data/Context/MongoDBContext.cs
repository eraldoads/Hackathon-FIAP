﻿using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Data.Context
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Agendamento> Agendamentos
        {
            get { return _database.GetCollection<Agendamento>(nameof(Agendamento)); }
        }
    }
}
