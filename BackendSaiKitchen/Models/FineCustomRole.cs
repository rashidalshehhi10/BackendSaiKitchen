using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineCustomRole
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string Alias { get; set; }
        public int CreationType { get; set; }
        public string Description { get; set; }
        public bool? Enable { get; set; }
        public int LastOperationType { get; set; }
        public string Name { get; set; }
    }
}
