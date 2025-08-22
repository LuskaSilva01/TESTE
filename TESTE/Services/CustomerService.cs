using MongoDB.Driver;   
using TESTE.Data; // se esse for o namespace onde está MongoDbService
using TESTE.Data.Entities; // Ajuste para o namespace da sua classe Customer

namespace TESTE.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(MongoDbService mongoDbService)
        {
            // Aqui definimos a coleção que será usada no MongoDB (nome "Customers")
            _customers = mongoDbService.Database.GetCollection<Customer>("Customers");
        }

        // Método para buscar todos os customers
        public async Task<List<Customer>> GetAsync()
        {
            return await _customers.Find(customer => true).ToListAsync();
        }

        // Método para buscar customer pelo Id
        public async Task<Customer?> GetByIdAsync(string id)
        {
            return await _customers.Find<Customer>(c => c.Id == id).FirstOrDefaultAsync();
        }

        // Método para criar um novo customer
        public async Task CreateAsync(Customer newCustomer)
        {
            await _customers.InsertOneAsync(newCustomer);
        }

        // Método para atualizar um customer existente
        public async Task UpdateAsync(string id, Customer updatedCustomer)
        {
            await _customers.ReplaceOneAsync(c => c.Id == id, updatedCustomer);
        }

        // Método para deletar um customer pelo Id
        public async Task RemoveAsync(string id)
        {
            await _customers.DeleteOneAsync(c => c.Id == id);
        }
    }
}
