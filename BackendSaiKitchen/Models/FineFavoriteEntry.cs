using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineFavoriteEntry
    {
        public string Id { get; set; }
        public string EntryId { get; set; }
        public DateTime? Time { get; set; }
        public string UserId { get; set; }
    }
}
