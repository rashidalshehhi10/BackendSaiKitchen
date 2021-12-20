using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineBackupNode
    {
        public string Id { get; set; }
        public DateTime? BackupEndTime { get; set; }
        public string BackupModule { get; set; }
        public string BackupName { get; set; }
        public DateTime? BackupTime { get; set; }
        public string Detail { get; set; }
        public string SavePath { get; set; }
        public double? BackupSize { get; set; }
        public int? Status { get; set; }
        public string Type { get; set; }
    }
}
