using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflowTaskImpl
    {
        public string Id { get; set; }
        public bool? Alerted { get; set; }
        public string CompleteState { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CurrentNodeIdx { get; set; }
        public DateTime? DeadLine { get; set; }
        public string FrTaskId { get; set; }
        public string Name { get; set; }
        public bool? NeedAllComplete { get; set; }
        public string NodeRoute { get; set; }
        public string Note { get; set; }
        public string OperatorJson { get; set; }
        public string OperatorOffset { get; set; }
        public string OperatorOffsetName { get; set; }
        public string ParentId { get; set; }
        public string ProcessId { get; set; }
        public int? ReportOffset { get; set; }
        public DateTime? SendTime { get; set; }
        public string Sender { get; set; }
        public string SenderId { get; set; }
        public string SonTaskId { get; set; }
        public int? State { get; set; }
        public string TaskBackTarget { get; set; }
        public string TaskId { get; set; }
    }
}
