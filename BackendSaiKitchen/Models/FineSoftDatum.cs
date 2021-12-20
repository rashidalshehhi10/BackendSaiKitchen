using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineSoftDatum
    {
        public string Id { get; set; }
        public string DeletedId { get; set; }
        public string DeletedName { get; set; }
        public int Type { get; set; }
    }
}
