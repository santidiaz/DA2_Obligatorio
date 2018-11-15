using System;

namespace LogContracts
{
    public class LogRecord
    {
        public string Action { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
    }
}