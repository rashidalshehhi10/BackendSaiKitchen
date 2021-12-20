using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSystemMessage
    {
        public string Id { get; set; }
        public long? Terminal { get; set; }
        public string Title { get; set; }
    }
}
