using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomPayment
    {
        
        public string PaymentName { get; set; }
        public string PaymentDetail { get; set; }
        public int? PaymentAmount { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? InquiryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public List<byte[]> base64 { get; set; }
    }

    public class UpdatePaymentStatus
    {
        public int InquiryId { get; set; }
        public int PaymentId { get; set; }
    }
}
