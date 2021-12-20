using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineMobileDevice
    {
        public string Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string DeviceName { get; set; }
        public string MacAddress { get; set; }
        public bool? Passed { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Username { get; set; }
    }
}
