using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineLastLogin
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Ip { get; set; }
        public DateTime? Time { get; set; }
        public string UserId { get; set; }
    }
}
