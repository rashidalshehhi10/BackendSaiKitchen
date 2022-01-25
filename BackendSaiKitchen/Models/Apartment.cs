using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Apartment
    {
        public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentDescription { get; set; }
        public int? CommercialProjectId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? UpdatedDate { get; set; }

        public virtual CommercialProject CommercialProject { get; set; }
    }
}
