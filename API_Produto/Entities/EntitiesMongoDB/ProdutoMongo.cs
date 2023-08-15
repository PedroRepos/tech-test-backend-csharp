using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Produto.Entities.EntitiesMongoDB
{
    /// <summary>
    /// Fornece as informações básicas para as conexões com o MongoDb | Provides the basic information for MongoDb connections
    /// </summary>
    public class ProdutoMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Nome")]
        public string Nome { get; set; } 

        [BsonElement("Preço")]
        public double Preco { get; set; }

        [BsonElement("QuantidadeEmEstoque")] 
        public int QuantidadeEmEstoque { get; set; }

        [BsonElement("DataDeCriação")]
        public DateTime DataDeCriacao { get; set; }
    }
}
