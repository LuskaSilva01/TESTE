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
            _customers = mongoDbService.Customers;
        }
        
        // Buscar todos os customers
        public List<Customer> GetAllCustomers() => _customers.Find(c => true).ToList();

        // Buscar customer pelo Id
        public Customer? GetById(string id) => _customers.Find(c => c.Id == id).FirstOrDefault();

        // Criar um novo customer
        public void AddCustomer(Customer customer) => _customers.InsertOne(customer);

        // Atualizar um customer existente
        public void UpdateCustomer(string id, Customer updatedCustomer) =>
            _customers.ReplaceOne(c => c.Id == id, updatedCustomer);

        // Deletar um customer pelo Id
        public void DeleteCustomer(string id) => _customers.DeleteOne(c => c.Id == id);
    }
}
