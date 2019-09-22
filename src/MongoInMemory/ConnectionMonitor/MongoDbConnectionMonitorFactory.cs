using System;

namespace MongoInMemory.ConnectionMonitor
{
    class MongoDbConnectionMonitorFactory
    {
        public IMongoDbConnectionMonitor CreateMongoDbConnectionMonitor()
        {
            return new DefaultMongoDbConnectionMonitor();
        }
    }
}
