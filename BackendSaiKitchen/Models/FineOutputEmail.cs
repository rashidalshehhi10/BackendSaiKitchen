using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineOutputEmail
    {
        public string Id { get; set; }
        public bool? AddLink { get; set; }
        public string BccAddress { get; set; }
        public string BodyContent { get; set; }
        public string CcAddress { get; set; }
        public string CustomAddress { get; set; }
        public string CustomBccAddress { get; set; }
        public string CustomCcAddress { get; set; }
        public bool? PreviewAttach { get; set; }
        public bool? PreviewWidget { get; set; }
        public string Subject { get; set; }
        public bool? UseAttach { get; set; }
    }
}
