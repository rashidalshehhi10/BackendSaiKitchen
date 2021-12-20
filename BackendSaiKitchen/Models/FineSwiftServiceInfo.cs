using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSwiftServiceInfo
    {
        public string Id { get; set; }
        public string ClusterId { get; set; }
        public bool? IsSingleton { get; set; }
        public string Service { get; set; }
        public string ServiceInfo { get; set; }
    }
}
