using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineAuthorityObject
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string ExpandId { get; set; }
        public int? ExpandType { get; set; }
        public string FullPath { get; set; }
        public string ParentId { get; set; }
        public string CoverId { get; set; }
        public string Description { get; set; }
        public int? DeviceType { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string MobileIcon { get; set; }
        public string Path { get; set; }
        public long? SortIndex { get; set; }
    }
}
