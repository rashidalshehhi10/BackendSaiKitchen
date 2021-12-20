using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineBlockIp
    {
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Ip { get; set; }
        public int? RejectedVisits { get; set; }
    }
}
