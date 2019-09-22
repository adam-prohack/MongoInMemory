using System;

namespace MongoInMemory.ProcessExecutor
{
    class ProcessExecutorFactory
    {
        public IProcessExecutor CreateProcessExecutor()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return new Windows.ProcessExecutor();
            throw new PlatformNotSupportedException();
        }
    }
}
