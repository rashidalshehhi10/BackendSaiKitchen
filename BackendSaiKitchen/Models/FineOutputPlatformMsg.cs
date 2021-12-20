using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineOutputPlatformMsg
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int? LinkOpenType { get; set; }
        public string Subject { get; set; }
    }
}
