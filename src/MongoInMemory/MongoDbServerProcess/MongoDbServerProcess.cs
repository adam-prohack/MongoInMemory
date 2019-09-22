using MongoInMemory.ProcessExecutor;

namespace MongoInMemory.MongoDbServerProcess
{
    internal class MongoDbServerProcess : IProcess
    {
        public string WorkingDirectory { get; set; }
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public int ServerPort { get; set; }
        public string ServerHost { get; set; }
    }
}
