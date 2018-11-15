using LogContracts;
using System;
using System.Collections.Generic;

namespace Logger
{
    public interface ILogger
    {
        void LogAction(string actionToLog, string username, DateTime dateTime);
        IList<LogRecord> GetLogs(DateTime initialDate, DateTime finalDate);
    }
}
