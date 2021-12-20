using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSwiftTablePath
    {
        public string ClusterId { get; set; }
        public string TableKey { get; set; }
        public int? LastPath { get; set; }
        public int? TablePath { get; set; }
        public int? TmpDir { get; set; }
    }
}
