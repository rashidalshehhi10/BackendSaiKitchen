using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflowTask
    {
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int? DeadLineDate { get; set; }
        public string DeadLineType { get; set; }
        public string IssueControl { get; set; }
        public bool? IssueOver { get; set; }
        public bool? LeapfrogBack { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string ProcessId { get; set; }
        public string RemindControl { get; set; }
        public bool? TaskNameCalculateOnce { get; set; }
    }
}
