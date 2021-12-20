using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineExtraProperty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RelatedId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
    }
}
