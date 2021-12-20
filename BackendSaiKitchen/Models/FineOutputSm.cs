using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineOutputSm
    {
        public string Id { get; set; }
        public string SmsParam { get; set; }
        public int? TemplateId { get; set; }
    }
}
