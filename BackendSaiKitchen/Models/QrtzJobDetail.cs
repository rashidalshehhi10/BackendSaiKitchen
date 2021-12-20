using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class QrtzJobDetail
    {
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string SchedName { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public bool IsDurable { get; set; }
        public bool IsNonconcurrent { get; set; }
        public bool IsUpdateData { get; set; }
        public string JobClassName { get; set; }
        public byte[] JobData { get; set; }
        public bool RequestsRecovery { get; set; }
    }
}
