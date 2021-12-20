using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineProcessMessage
    {
        public string Id { get; set; }
        public string AllTaskId { get; set; }
        public DateTime? DeadLine { get; set; }
        public bool? Processed { get; set; }
        public string TaskId { get; set; }
    }
}
