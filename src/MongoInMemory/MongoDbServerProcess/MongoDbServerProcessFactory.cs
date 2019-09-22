using MongoInMemory.Utils;
using System;
using System.IO;

namespace MongoInMemory.MongoDbServerProcess
{
    internal class MongoDbServerProcessFactory
    {
        private string getTempWorkingDirectory()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            if (Directory.Exists(tempPath)) { Directory.Delete(tempPath, true); }
            Directory.CreateDirectory(tempPath);
            return tempPath;
        }
        private string getMongoDbDeamonPath()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                return Path.Combine(Directory.GetCurrentDirectory(), "resources", "win32-x86_64-2012plus", "mongod.exe");
            else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("ubuntu"))
                return Path.Combine(Directory.GetCurrentDirectory(), "resources", "linux-x86_64-ubuntu1804", "mongod");
            else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("osx"))
                return Path.Combine(Directory.GetCurrentDirectory(), "resources", "macos-x86_64", "mongod");
            else
                throw new NotSupportedException($"Current operating system is not supported");
        }
        private string getMongoDbServerArguments(int port, string ipAddress, string cwd)
        {
            return $"--port {port} --bind_ip {ipAddress} --dbpath {cwd}";
        }

        public MongoDbServerProcess CreateMongoDbServerProcess()
        {
            var host = "127.0.0.1";
            var port = PortUtils.GetFreePortNumber();
            var workingDirectory = getTempWorkingDirectory();
            return new MongoDbServerProcess()
            {
                WorkingDirectory = workingDirectory,
                FileName = getMongoDbDeamonPath(),
                Arguments = getMongoDbServerArguments(port, host, workingDirectory),
                ServerPort = port,
                ServerHost = host
            };
        }
    }
}
