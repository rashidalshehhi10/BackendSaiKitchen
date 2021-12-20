using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineScheduleTaskLog
    {
        public string Id { get; set; }
        public long LogTime { get; set; }
        public int? LogType { get; set; }
        public string TaskId { get; set; }
        public string TaskLog { get; set; }
        public string TaskName { get; set; }
    }
}
