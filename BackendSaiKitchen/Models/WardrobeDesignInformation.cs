using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class WardrobeDesignInformation
    {
        public WardrobeDesignInformation()
        {
            Accesories = new HashSet<Accesory>();
            Measurements = new HashSet<Measurement>();
        }

        public int Wdiid { get; set; }
        public string WdiclosetType { get; set; }
        public string WdiboardModel { get; set; }
        public string WdiselectedColor { get; set; }
        public string WdiceilingHeight { get; set; }
        public string WdiclosetHeight { get; set; }
        public bool? WdistorageDoor { get; set; }
        public string WdidoorDesign { get; set; }
        public string WdihandleDesign { get; set; }
        public string WdidoorMaterial { get; set; }
        public string Wdinotes { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Accesory> Accesories { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
