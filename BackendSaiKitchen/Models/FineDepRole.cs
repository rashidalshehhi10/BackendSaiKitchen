using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineDepRole
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public int CreationType { get; set; }
        public string DepartmentId { get; set; }
        public string FullPath { get; set; }
        public int LastOperationType { get; set; }
        public string PostId { get; set; }
    }
}
