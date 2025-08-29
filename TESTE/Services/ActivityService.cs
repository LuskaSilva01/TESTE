using MongoDB.Driver;
using TESTE.Data;
using TESTE.Data.Entities;

namespace TESTE.Services
{
    public class ActivityService
    {
        private readonly IMongoCollection<Activity> _activities;

        public ActivityService(MongoDbService mongoDbService)
        {
            // Usando a collection já exposta pelo MongoDbService
            _activities = mongoDbService.Activities;
        }

        // Buscar todas as activities
        public List<Activity> GetAllActivities() => _activities.Find(a => true).ToList();

        // Buscar activity pelo Id
        public Activity? GetById(string id) => _activities.Find(a => a.Id == id).FirstOrDefault();

        // Buscar todas as activities de um cliente pelo CustomerId
        public List<Activity> GetByCustomerId(string customerId) =>
            _activities.Find(a => a.CustomerId == customerId).ToList();
            
        // Criar uma nova activity
        public void AddActivity(Activity activity) => _activities.InsertOne(activity);

        // Atualizar uma activity existente
        public void UpdateActivity(string id, Activity updatedActivity) =>
            _activities.ReplaceOne(a => a.Id == id, updatedActivity);

        // Deletar uma activity pelo Id
        public void DeleteActivity(string id) => _activities.DeleteOne(a => a.Id == id);
    }
}
