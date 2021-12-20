using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineLoginLock
    {
        public string Id { get; set; }
        public int? ErrorTime { get; set; }
        public string LockObject { get; set; }
        public string LockObjectValue { get; set; }
        public DateTime? LockTime { get; set; }
        public bool? Locked { get; set; }
        public DateTime? UnlockTime { get; set; }
        public string UserId { get; set; }
    }
}
