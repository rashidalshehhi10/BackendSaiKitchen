#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class PermissionRole
    {
        public int PermissionRoleId { get; set; }
        public int? PermissionId { get; set; }
        public int? BranchRoleId { get; set; }
        public int? PermissionLevelId { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual BranchRole BranchRole { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual PermissionLevel PermissionLevel { get; set; }
    }
}
