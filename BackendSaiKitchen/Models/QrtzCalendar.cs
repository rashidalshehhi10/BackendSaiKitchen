using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzCalendar
    {
        public string CalendarName { get; set; }
        public string SchedName { get; set; }
        public byte[] Calendar { get; set; }
    }
}
