using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelog.Agent.ViewModels
{
    class Log
    {
        public string EnrolleeId { get; set; }
        public string DeviceId { get; set; }
        public DateTimeOffset TimeLog { get; set; }
    }
}
