namespace API_Produto.Entities.EntitiesMongoDB
{
    /// <summary>
    /// Fornece as informações básicas para as conexões com o MongoDb | Provides the basic information for MongoDb connections
    /// </summary>
    public class ProdutoDatabaseSetting
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string ProdutoCollectionName { get; set; } = null;
    }
}
