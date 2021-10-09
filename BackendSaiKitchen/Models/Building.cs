using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Building
    {
        public Building()
        {
            Inquiries = new HashSet<Inquiry>();
        }

        public int BuildingId { get; set; }
        public string BuildingAddress { get; set; }
        public string BuildingTypeOfUnit { get; set; }
        public string BuildingCondition { get; set; }
        public string BuildingFloor { get; set; }
        public bool? BuildingReconstruction { get; set; }
        public bool? IsOccupied { get; set; }
        public string BuildingMakaniMap { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
