using MongoInMemory.ConnectionMonitor;
using MongoInMemory.MongoDbServerProcess;
using MongoInMemory.ProcessExecutor;

namespace MongoInMemory
{
    public static class MongoDbRunner
    {
        public static MongoDbServerDescriptor CreateMongoDbServerInstance()
        {
            var processExecutorFactory = new ProcessExecutorFactory();
            var mongoDbServerProcessFactory = new MongoDbServerProcessFactory();
            var mongoDbConnectionMonitorFactory = new MongoDbConnectionMonitorFactory();

            var processExecutor = processExecutorFactory.CreateProcessExecutor();
            var mongoDbServerProcess = mongoDbServerProcessFactory.CreateMongoDbServerProcess();
            processExecutor.StartProcess(mongoDbServerProcess);

            var mongoDbConnectionMonitor = mongoDbConnectionMonitorFactory.CreateMongoDbConnectionMonitor();
            mongoDbConnectionMonitor.WaitForMongoDbServer(mongoDbServerProcess.ServerHost, mongoDbServerProcess.ServerPort);

            return new MongoDbServerDescriptor()
            {
                Host = mongoDbServerProcess.ServerHost,
                Port = mongoDbServerProcess.ServerPort
            };
        }
    }
}
