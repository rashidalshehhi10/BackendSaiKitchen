using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FinePrintOffsetIpRelate
    {
        public string Id { get; set; }
        public string ChildIp { get; set; }
        public string MotherId { get; set; }
    }
}
