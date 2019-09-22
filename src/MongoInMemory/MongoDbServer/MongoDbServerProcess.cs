using MongoInMemory.ProcessExecutor;

namespace MongoInMemory.MongoDbServer
{
    public class MongoDbServerProcess : IProcess
    {
        public string WorkingDirectory { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public int ServerPort { get; internal set; }
    }
}
