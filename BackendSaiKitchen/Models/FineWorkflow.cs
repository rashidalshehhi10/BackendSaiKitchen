using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineWorkflow
    {
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string NodesId { get; set; }
    }
}
