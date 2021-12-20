using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzTrigger
    {
        public string SchedName { get; set; }
        public string TriggerGroup { get; set; }
        public string TriggerName { get; set; }
        public string AppointId { get; set; }
        public string CalendarName { get; set; }
        public string Description { get; set; }
        public decimal EndTime { get; set; }
        public byte[] JobData { get; set; }
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public int? MisfireInstr { get; set; }
        public decimal? NextFireTime { get; set; }
        public decimal? PrevFireTime { get; set; }
        public int? Priority { get; set; }
        public decimal StartTime { get; set; }
        public string TriggerState { get; set; }
        public string TriggerType { get; set; }
    }
}
