using System;
using System.Diagnostics;

namespace MongoInMemory.ProcessExecutor.Windows
{
    internal class ProcessExecutor : IProcessExecutor
    {
        public void StartProcess(IProcess process)
        {
            var job = new Job();
            var childProcess = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WorkingDirectory = process.WorkingDirectory,
                    FileName = process.FileName,
                    Arguments = process.Arguments
                }
            };
            childProcess.Start();
            job.AddProcess(childProcess.Handle);
        }
    }
}
