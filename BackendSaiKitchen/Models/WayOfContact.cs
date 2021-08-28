using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class WayOfContact
    {
        public WayOfContact()
        {
            Customers = new HashSet<Customer>();
        }

        public int WayOfContactId { get; set; }
        public string WayOfContactName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
