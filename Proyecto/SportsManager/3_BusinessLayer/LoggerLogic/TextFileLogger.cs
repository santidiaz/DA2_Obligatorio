using LogContracts;
using Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LoggerLogic
{
    public class TextFileLogger : ILogger
    {
        private readonly string fileLogPath;
        private readonly char splitter = '|';
        private const string DATE_FORMAT = "dd/MM/yyyy hh:mm";
        public TextFileLogger()
        {
            string rootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
            this.fileLogPath = string.Concat(rootPath, @"Resources\Logs\LogFile.txt");

        }

        public IList<LogRecord> GetLogs(DateTime initialDate, DateTime finalDate)
        {
            var loggedRecords = new List<LogRecord>();
            if (File.Exists(fileLogPath))
            {
                var fileStream = new FileStream(this.fileLogPath, FileMode.Open, FileAccess.Read);
                try
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        streamReader.ReadLine(); // To avoid the header.
                        string line;
                        string[] splittedLine;
                        DateTime currentLogDate;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            splittedLine = line.Split(splitter);
                            currentLogDate = DateTime.ParseExact(splittedLine[1], DATE_FORMAT, CultureInfo.InvariantCulture);
                            if (this.IsLogBetweenDates(initialDate, finalDate, currentLogDate))
                            {
                                loggedRecords.Add(
                                    new LogRecord
                                    {
                                        Action = splittedLine[0],
                                        LogDate = currentLogDate,
                                        UserName = splittedLine[2]
                                    });
                            }
                        }
                    }
                }
                catch (IOException ioEx)
                {
                    throw ioEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    fileStream.Close();
                }
            }

            return loggedRecords;
        }

        public void LogAction(string actionToLog, string username, DateTime dateTime)
        {
            if (!File.Exists(fileLogPath))
            {
                using (StreamWriter sWriter = File.AppendText(fileLogPath))
                {
                    sWriter.WriteLine("[Action | DateTime | UserName]");
                    sWriter.WriteLine("{0}|{1}|{2}",
                        actionToLog,
                        dateTime.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
                        username);
                }
            }
            else
            {
                using (StreamWriter sWriter = File.AppendText(fileLogPath))
                {
                    sWriter.WriteLine("{0}|{1}|{2}",
                        actionToLog,
                        dateTime.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
                        username);
                }
            }
        }

        #region Private Methods
        private bool IsLogBetweenDates(DateTime initialDate, DateTime finalDate, DateTime logDate)
        {
            return logDate >= initialDate && logDate <= finalDate;
        }
        #endregion
    }
}
