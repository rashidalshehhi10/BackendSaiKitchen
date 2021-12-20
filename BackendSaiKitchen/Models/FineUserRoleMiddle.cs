using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineUserRoleMiddle
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string RoleId { get; set; }
        public int RoleType { get; set; }
        public string UserId { get; set; }
    }
}
