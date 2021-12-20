using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineScheduleRecord
    {
        public string Id { get; set; }
        public string Creator { get; set; }
        public string DetailMessage { get; set; }
        public string FilePath { get; set; }
        public string LogMessage { get; set; }
        public DateTime? LogTime { get; set; }
        public int? LogType { get; set; }
        public DateTime? NextFireTime { get; set; }
        public int? RunType { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
    }
}
