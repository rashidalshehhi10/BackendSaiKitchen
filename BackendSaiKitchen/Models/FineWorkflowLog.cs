using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflowLog
    {
        public string Id { get; set; }
        public DateTime? DateTime { get; set; }
        public string Message { get; set; }
        public string OperatorName { get; set; }
        public string ProcessName { get; set; }
        public string TaskName { get; set; }
    }
}
