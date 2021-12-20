using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineBaseOutput
    {
        public string Id { get; set; }
        public string ActionName { get; set; }
        public bool ExecuteByUser { get; set; }
        public string OutputId { get; set; }
        public string ResultUrl { get; set; }
        public int RunType { get; set; }
    }
}
