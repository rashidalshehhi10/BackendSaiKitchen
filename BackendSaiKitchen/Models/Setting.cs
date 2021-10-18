#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string SettingDescription { get; set; }
        public int? SettingMeasurementDelay { get; set; }
        public int? SettingDesignDelay { get; set; }
        public int? SettingQuotationDelay { get; set; }
        public int? SettingNoActionDelayFromCustomer { get; set; }
        public int? SettingMaintenanceAfterMonth { get; set; }
        public int? SettingAssigneeDelay { get; set; }
        public int? SettingApprovalDelay { get; set; }
        public int? SettingCustomerContactDelay { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
