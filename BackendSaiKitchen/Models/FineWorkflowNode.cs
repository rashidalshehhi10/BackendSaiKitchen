using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflowNode
    {
        public string Id { get; set; }
        public string AlertControl { get; set; }
        public string Authority { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool? NeedAllComplete { get; set; }
        public bool? NeedOfflineReport { get; set; }
        public string ProcessId { get; set; }
        public string ReportControl { get; set; }
    }
}
