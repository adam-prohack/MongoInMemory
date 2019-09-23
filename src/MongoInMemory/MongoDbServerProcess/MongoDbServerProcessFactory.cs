using MongoInMemory.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

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
            var basePathes = new List<string>()
            {
                Directory.GetCurrentDirectory(),
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(MongoDbServerProcessFactory)).Location),
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)
            };
            foreach (var basePath in basePathes.Distinct())
            {
                var resultPath = "";
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    resultPath = Path.Combine(basePath, "resources", "mongodb-win32-x86_64-2012plus", "mongod.exe");
                else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("ubuntu"))
                    resultPath = Path.Combine(basePath, "resources", "mongodb-linux-x86_64-ubuntu1804", "mongod");
                else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("osx"))
                    resultPath = Path.Combine(basePath, "resources", "mongodb-macos-x86_64", "mongod");
                else
                    throw new NotSupportedException($"Current operating system is not supported");
                Debug.WriteLine(resultPath);
                if (File.Exists(resultPath)) { return resultPath; }
            }
            throw new FileNotFoundException($"Couldn't find mongod file");

            //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            //    return Path.Combine(libAssemblyLocation, "resources", "win32-x86_64-2012plus", "mongod.exe");
            //else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("ubuntu"))
            //    return Path.Combine(libAssemblyLocation, "resources", "linux-x86_64-ubuntu1804", "mongod");
            //else if (Environment.OSVersion.Platform == PlatformID.Unix && Environment.OSVersion.VersionString.ToLower().Contains("osx"))
            //    return Path.Combine(libAssemblyLocation, "resources", "macos-x86_64", "mongod");
            //else
            //    throw new NotSupportedException($"Current operating system is not supported");
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
