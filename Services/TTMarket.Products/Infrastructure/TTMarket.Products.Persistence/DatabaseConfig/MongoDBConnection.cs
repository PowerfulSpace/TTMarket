namespace TTMarket.Products.Persistence.DatabaseConfig
{
    internal class MongoDBConnection : IMongoDBConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}