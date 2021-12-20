using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FinePrintOffset
    {
        public string Id { get; set; }
        public string CptName { get; set; }
        public string Ip { get; set; }
        public string OffsetX { get; set; }
        public string OffsetY { get; set; }
        public string Sign { get; set; }
    }
}
