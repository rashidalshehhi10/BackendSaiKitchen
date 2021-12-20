using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzSimpleTrigger
    {
        public string SchedName { get; set; }
        public string TriggerGroup { get; set; }
        public string TriggerName { get; set; }
        public decimal RepeatCount { get; set; }
        public decimal RepeatInterval { get; set; }
        public decimal TimesTriggered { get; set; }
    }
}
