using LogContracts;
using LoggerLogic;
using System;

namespace LogProvider
{
    public class Logger
    {
        private readonly ILogger loggerLogic;

        #region Singleton
        private static readonly Lazy<Logger> currentInstance = new Lazy<Logger>(() => new Logger());
        private Logger()
        {
            loggerLogic = new TextFileLogger();
        }
        public static Logger GetInstance
        {
            get
            {
                return currentInstance.Value;
            }
        }
        #endregion

        public ILogger LogTool()
        {
            return this.loggerLogic;
        }
    }
}
