using API_Produto.Entities.EntitiesMongoDB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_Produto.ServicesMongoDB
{
    /// <summary>
    /// Cria toda a configuração para o execicío das operações CRUD | Creates all the configuration for the execution of CRUD operations
    /// </summary>
    public class ProdutoService
    {
        private readonly IMongoCollection<ProdutoMongo> _produtoCollection;

        public ProdutoService(IOptions<ProdutoDatabaseSetting> produtoServices)
        {
            var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

            _produtoCollection = mongoDatabase.GetCollection<ProdutoMongo>(produtoServices.Value.ProdutoCollectionName);
        }

        public async Task<List<ProdutoMongo>> GetAsync() =>
            await _produtoCollection.Find(x => true).ToListAsync();

        public async Task CreateAsync(ProdutoMongo produto) =>
            await _produtoCollection.InsertOneAsync(produto);

        public async Task UpdateAsync(string id, ProdutoMongo produto) =>
            await _produtoCollection.ReplaceOneAsync(x => x.Id == id, produto);

        public async Task RemoveAsync(string id) =>
            await _produtoCollection.DeleteOneAsync(x => x.Id == id);

    }
}
