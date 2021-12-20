using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflowStashDatum
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string ReportPath { get; set; }
        public string TaskId { get; set; }
        public string UserId { get; set; }
    }
}
