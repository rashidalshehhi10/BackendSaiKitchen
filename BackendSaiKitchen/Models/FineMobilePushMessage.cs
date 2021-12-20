using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineMobilePushMessage
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string MediaId { get; set; }
        public int? MsgType { get; set; }
        public int? Terminal { get; set; }
        public string Title { get; set; }
    }
}
