namespace MongoInMemory.ConnectionMonitor
{
    internal class MongoDbConnectionMonitorFactory
    {
        public IMongoDbConnectionMonitor CreateMongoDbConnectionMonitor()
        {
            return new DefaultMongoDbConnectionMonitor();
        }
    }
}
