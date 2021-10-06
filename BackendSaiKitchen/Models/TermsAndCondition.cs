#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class TermsAndCondition
    {
        public int TermsAndConditionsId { get; set; }
        public string TermsAndConditionsDetail { get; set; }
        public bool? IsInstallmentTerms { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
