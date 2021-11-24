using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string CommentName { get; set; }
        public string CommentDetail { get; set; }
        public int? InquiryId { get; set; }
        public int? InquiryStatusId { get; set; }
        public int? CommentAddedBy { get; set; }
        public string CommentAddedon { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual User CommentAddedByNavigation { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public virtual InquiryStatus InquiryStatus { get; set; }
    }
}
