using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineUsageDatum
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public int DataType { get; set; }
        public int SubType { get; set; }
        public string Tag { get; set; }
    }
}
