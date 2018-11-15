using System;
using System.Collections.Generic;

namespace LogContracts
{
    public interface ILogger
    {
        void LogAction(string actionToLog, string username, DateTime dateTime);
        IList<LogRecord> GetLogs(DateTime initialDate, DateTime finalDate);
    }
}