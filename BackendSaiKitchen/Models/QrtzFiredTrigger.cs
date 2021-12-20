using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzFiredTrigger
    {
        public string EntryId { get; set; }
        public string SchedName { get; set; }
        public decimal FiredTime { get; set; }
        public string InstanceName { get; set; }
        public bool IsNonconcurrent { get; set; }
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public int? Priority { get; set; }
        public bool RequestsRecovery { get; set; }
        public decimal SchedTime { get; set; }
        public string State { get; set; }
        public string TriggerGroup { get; set; }
        public string TriggerName { get; set; }
    }
}
