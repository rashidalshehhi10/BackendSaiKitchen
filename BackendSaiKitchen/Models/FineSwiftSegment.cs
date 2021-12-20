using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSwiftSegment
    {
        public string Id { get; set; }
        public int? SegmentOrder { get; set; }
        public string SegmentOwner { get; set; }
        public string SegmentUri { get; set; }
        public string StoreType { get; set; }
        public string SwiftSchema { get; set; }
    }
}
