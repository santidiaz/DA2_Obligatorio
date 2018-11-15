using LogContracts;
using Logger;
using System;
using System.Collections.Generic;
using System.IO;

namespace LoggerLogic
{
    public class TextFileLogger : ILogger
    {
        private readonly string logPath;
        public TextFileLogger()
        {
            string rootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
            this.logPath = string.Concat(rootPath, @"\\Resources\\Logs");
        }

        public IList<LogRecord> GetLogs(DateTime initialDate, DateTime finalDate)
        {
            

            

            throw new NotImplementedException();
        }
        public void LogAction(LogRecord logToBeRecorded)
        {
            string filePath = string.Concat(logPath, "\\LogFile.txt");
            using (StreamWriter sWriter = File.AppendText(filePath))
            {
                sWriter.Write("\r\n[Action | DateTime | UserName] : ");
                sWriter.WriteLine("{0}|{1}|{2}", 
                    logToBeRecorded.Action, 
                    logToBeRecorded.LogDate.ToString("dd/MM/yyyy hh:mm"),
                    logToBeRecorded.UserName);

            }
        }
    }
}
