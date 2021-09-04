using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class CalendarEvent
    {
        public int CalendarEventId { get; set; }
        public string CalendarEventName { get; set; }
        public string CalendarEventDescription { get; set; }
        public string CalendarEventOnClickUrl { get; set; }
        public string CalendarEventDate { get; set; }
        public int? EventTypeId { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual User User { get; set; }
    }
}
