using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineUser
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public DateTime? Birthday { get; set; }
        public int CreationType { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool? Enable { get; set; }
        public string Language { get; set; }
        public int LastOperationType { get; set; }
        public bool? Male { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string RealAlias { get; set; }
        public string RealName { get; set; }
        public string Salt { get; set; }
        public string UserAlias { get; set; }
        public string UserName { get; set; }
        public string WorkPhone { get; set; }
    }
}
