using System;
using Microsoft.Extensions.Options;
using System.Data.Common;
using MongoDB.Driver;
using TESTE.Settings;
using TESTE.Data.Entities;

namespace TESTE.Data;

public class MongoDbService
{
    private readonly IConfiguration _configuration;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Customer> _customers;
    private readonly IMongoCollection<Activity> _activities;

    public MongoDbService(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration.GetConnectionString("DbConnection");
        var databaseName = _configuration["MongoDbSettings:DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);

        _customers = _database.GetCollection<Customer>("Customers");
        _activities = _database.GetCollection<Activity>("Activities");
    }
    public List<Customer> GetAllCustomers()
    {
        return _customers.Find(c => true).ToList();
    }
    public void AddCustomer(Customer customer)
    {
        _customers.InsertOne(customer);
    }

    public List<Activity> GetAllActivities()
    {
        return _activities.Find(a => true).ToList();
    }

    public void AddActivity(Activity activity)
    {
        _activities.InsertOne(activity);
    }

    public List<Activity> GetActivitiesByCustomerId(string customerId)
    {
        return _activities.Find(a => a.CustomerId == customerId).ToList();
    }

    // Você pode manter essa propriedade para expor o Database, se quiser:}

    public IMongoDatabase Database => _database;

}

