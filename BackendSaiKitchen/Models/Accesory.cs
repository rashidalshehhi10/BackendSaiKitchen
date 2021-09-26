#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Accesory
    {
        public int AccesoriesId { get; set; }
        public int? WardrobeDesignInfoId { get; set; }
        public string AccesoriesName { get; set; }
        public bool? AccesoriesValue { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual WardrobeDesignInformation WardrobeDesignInfo { get; set; }
    }
}
