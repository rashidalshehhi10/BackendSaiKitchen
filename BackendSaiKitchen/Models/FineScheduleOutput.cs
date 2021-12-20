using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineScheduleOutput
    {
        public string Id { get; set; }
        public string BaseName { get; set; }
        public bool? CreateAttachByUsername { get; set; }
        public string Formats { get; set; }
    }
}
