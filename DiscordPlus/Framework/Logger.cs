using DiscordRPC.Logging;
using StardewModdingAPI;
using System;
using RPCLogLevel = DiscordRPC.Logging.LogLevel;
using SDVLogLevel = StardewModdingAPI.LogLevel;

namespace BlakeIsMyWaifu.Stardew.DiscordPlus
{
    public class Logger : ILogger
    {
        public RPCLogLevel Level { get; set; }
        private readonly IMonitor _monitor;

        public Logger(IMonitor monitor)
        {
            _monitor = monitor;
        }

        public void Trace(string message, params object[] args)
        {
            Log(message, args, SDVLogLevel.Trace);
        }

        public void Info(string message, params object[] args)
        {
            Log(message, args, SDVLogLevel.Info);
        }

        public void Warning(string message, params object[] args)
        {
            Log(message, args, SDVLogLevel.Warn);
        }

        public void Error(string message, params object[] args)
        {
            Log(message, args, SDVLogLevel.Error);
        }

        private void Log(string message, object[] args, SDVLogLevel level)
        {
            if (_monitor.IsVerbose)
            {
                _monitor.Log("DiscordPlus: " + String.Format(message, args), level);
            }
        }
    }
}
