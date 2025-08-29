using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TESTE.Data.Entities;

public class Activity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = null!;

    [BsonElement("descricao")]
    public string Descricao { get; set; } = null!;

    [BsonElement("data")]
    public DateTime Data { get; set; }
}
