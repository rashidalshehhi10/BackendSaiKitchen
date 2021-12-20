using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzLock
    {
        public string LockName { get; set; }
        public string SchedName { get; set; }
    }
}
