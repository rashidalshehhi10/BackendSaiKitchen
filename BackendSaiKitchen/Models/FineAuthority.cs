using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineAuthority
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public int Authority { get; set; }
        public string AuthorityEntityId { get; set; }
        public int AuthorityEntityType { get; set; }
        public int AuthorityType { get; set; }
        public string RoleId { get; set; }
        public int RoleType { get; set; }
    }
}
