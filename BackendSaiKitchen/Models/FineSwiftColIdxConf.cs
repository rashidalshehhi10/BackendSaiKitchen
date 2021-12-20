using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSwiftColIdxConf
    {
        public string ColumnName { get; set; }
        public string TableKey { get; set; }
        public bool? RequireGlobalDict { get; set; }
        public bool? RequireIndex { get; set; }
    }
}
