using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSwiftMetadatum
    {
        public string Id { get; set; }
        public string Fields { get; set; }
        public string Remark { get; set; }
        public string SchemaName { get; set; }
        public string SwiftSchema { get; set; }
        public string TableName { get; set; }
    }
}
