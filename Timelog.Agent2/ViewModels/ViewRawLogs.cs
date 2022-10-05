using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timelog.Agent.ViewModels
{
    public class ViewRawLogs
    {
        public Guid TenantId { get; set; }
        public string DeviceCode { get; set; }
        public List<LogList> LogList { get; set; }
    }

    public class LogList
    {
        public string EnrollNumber { get; set; }
        public DateTimeOffset LogTime { get; set; }
    }
}
