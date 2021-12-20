using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzSchedulerState
    {
        public string InstanceName { get; set; }
        public string SchedName { get; set; }
        public decimal CheckinInterval { get; set; }
        public decimal LastCheckinTime { get; set; }
    }
}
