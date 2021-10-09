using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class EventType
    {
        public EventType()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
        }

        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public string EventTypeDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
    }
}
