namespace TTMarket.Products.Persistence.DatabaseConfig
{
    internal class MongoDBConnection : IMongoDBConnection
    {
        public string ProductsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}