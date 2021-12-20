using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineVc
    {
        public string Id { get; set; }
        public string CommitCode { get; set; }
        public string CommitMsg { get; set; }
        public string Filename { get; set; }
        public DateTime? Time { get; set; }
        public string Username { get; set; }
        public int? Version { get; set; }
    }
}
