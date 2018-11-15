using System;
using System.Collections.Generic;
using System.Text;

namespace LogContracts
{
    public class LogRecord
    {
        public string Action { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
    }
}
