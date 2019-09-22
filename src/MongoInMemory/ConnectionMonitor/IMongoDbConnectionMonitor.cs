namespace MongoInMemory.ConnectionMonitor
{
    public interface IMongoDbConnectionMonitor
    {
        void WaitForMongoDbServer(string host, int port);
    }
}
