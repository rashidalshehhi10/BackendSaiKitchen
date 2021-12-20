using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineOutputClientNotice
    {
        public string Id { get; set; }
        public string Addressee { get; set; }
        public string Content { get; set; }
        public string CustomizeLink { get; set; }
        public int? LinkOpenType { get; set; }
        public string MediaId { get; set; }
        public string Subject { get; set; }
        public int? Terminal { get; set; }
        public int? Type { get; set; }
    }
}
