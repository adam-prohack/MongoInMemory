using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MongoInMemory.ConnectionMonitor
{
    internal class DefaultMongoDbConnectionMonitor : IMongoDbConnectionMonitor
    {
        private readonly static TimeSpan Timeout = TimeSpan.FromSeconds(30);

        public void WaitForMongoDbServer(string host, int port)
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var mongoClient = new MongoClient(new MongoClientSettings() { Server = new MongoServerAddress(host, port) });
                        var dbNames = mongoClient.ListDatabaseNames().ToList();
                        break;
                    }
                    catch (MongoConnectionException) { Thread.Sleep(TimeSpan.FromMilliseconds(100)); }
                }
            });
            if (!task.Wait(Timeout)) { throw new TimeoutException("Connecting to mongo db instance timed out"); }
        }
    }
}