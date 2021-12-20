using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineLabel
    {
        public string Id { get; set; }
        public string LabelName { get; set; }
        public int RelatedType { get; set; }
    }
}
