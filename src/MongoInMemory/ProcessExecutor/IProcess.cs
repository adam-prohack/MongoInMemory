namespace MongoInMemory.ProcessExecutor
{
    internal interface IProcess
    {
        string WorkingDirectory { get; }
        string FileName { get; }
        string Arguments { get; }
    }
}
