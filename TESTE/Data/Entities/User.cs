using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;  

namespace TESTE.Data.Entities
{
    public class User
    {
        [BsonId] // Define que este é o Id no Mongo
        [BsonRepresentation(BsonType.ObjectId)] // Faz a conversão automática para string
        public string? Id { get; set; } // <- Torna o campo opcional (nullable)

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? Telefone { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; } = null;
       
    }
}
