using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class User
    {
        public User()
        {
            CalendarEvents = new HashSet<CalendarEvent>();
            Customers = new HashSet<Customer>();
            DesignDesignApprovedByNavigations = new HashSet<Design>();
            DesignDesignTakenByNavigations = new HashSet<Design>();
            InquiryAddedByNavigations = new HashSet<Inquiry>();
            InquiryQuotationAssignToNavigations = new HashSet<Inquiry>();
            InquiryWorkscopeDesignAssignedToNavigations = new HashSet<InquiryWorkscope>();
            InquiryWorkscopeMeasurementAssignedToNavigations = new HashSet<InquiryWorkscope>();
            MeasurementMeasurementApprovedByNavigations = new HashSet<Measurement>();
            MeasurementMeasurementTakenByNavigations = new HashSet<Measurement>();
            Notifications = new HashSet<Notification>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserMobile { get; set; }
        public string UserToken { get; set; }
        public string UserProfileImageUrl { get; set; }
        public string UserFcmtoken { get; set; }
        public string LastSeen { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsOnline { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Design> DesignDesignApprovedByNavigations { get; set; }
        public virtual ICollection<Design> DesignDesignTakenByNavigations { get; set; }
        public virtual ICollection<Inquiry> InquiryAddedByNavigations { get; set; }
        public virtual ICollection<Inquiry> InquiryQuotationAssignToNavigations { get; set; }
        public virtual ICollection<InquiryWorkscope> InquiryWorkscopeDesignAssignedToNavigations { get; set; }
        public virtual ICollection<InquiryWorkscope> InquiryWorkscopeMeasurementAssignedToNavigations { get; set; }
        public virtual ICollection<Measurement> MeasurementMeasurementApprovedByNavigations { get; set; }
        public virtual ICollection<Measurement> MeasurementMeasurementTakenByNavigations { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
