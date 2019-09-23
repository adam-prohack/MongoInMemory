namespace MongoInMemory.ConnectionMonitor
{
    internal interface IMongoDbConnectionMonitor
    {
        void WaitForMongoDbServer(string host, int port);
    }
}
