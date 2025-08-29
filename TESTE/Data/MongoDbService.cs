using System;
using Microsoft.Extensions.Options;
using System.Data.Common;
using MongoDB.Driver;
using TESTE.Settings;
using TESTE.Data.Entities;

namespace TESTE.Data;

public class MongoDbService
{
    private readonly IMongoDatabase _database;
     
    public MongoDbService(IOptions<MongoDbSettings> settings)
    {

        var client = new MongoClient(settings.Value.Dbconnection);
        _database = client.GetDatabase(settings.Value.DatabaseName);

    }
   
   public IMongoCollection<User> Users => _database.GetCollection<User>("Usuarios");
        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
        public IMongoCollection<Activity> Activities => _database.GetCollection<Activity>("Activities");

    public IMongoDatabase Database => _database;

}

