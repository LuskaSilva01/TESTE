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

        // Buscar todas as activities de um usuario pelo Userid
        public List<Activity> GetByUserid(string Userid) =>
            _activities.Find(a => a.UserId == Userid).ToList();
            
        // Criar uma nova activity
        public void AddActivity(Activity activity) => _activities.InsertOne(activity);

        // Atualizar uma activity existente
        public void UpdateActivity(string id, Activity updatedActivity) =>
            _activities.ReplaceOne(a => a.Id == id, updatedActivity);

    }
}
