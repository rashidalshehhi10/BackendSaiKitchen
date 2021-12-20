using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineBaseMessage
    {
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? Datetime { get; set; }
        public string Message { get; set; }
        public bool? Readed { get; set; }
        public bool? Toasted { get; set; }
        public int? Type { get; set; }
        public string Url { get; set; }
        public int? UrlType { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
