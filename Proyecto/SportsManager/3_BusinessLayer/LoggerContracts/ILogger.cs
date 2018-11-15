using LogContracts;
using System;
using System.Collections.Generic;

namespace Logger
{
    public interface ILogger
    {
        void LogAction(LogRecord logToBeRecorded);
        IList<LogRecord> GetLogs(DateTime initialDate, DateTime finalDate);
    }
}
