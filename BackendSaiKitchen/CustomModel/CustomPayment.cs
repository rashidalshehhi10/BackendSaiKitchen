using System;
using System.Collections.Generic;

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
    public class SalesInvoiceRequest
    {
        public int PaymentId { get; set; }
        public int PaymentModeId { get; set; }
    }

    public class Invoice
    {
        public int PaymentId { get; set; }
        public int PaymentModeId { get; set; }
        public List<byte[]> Files { get; set; }
    }

    public class SalesInvoiceReciept
    {
        public String InvoiceCode { get; set; }
        public String InquiryCode { get; set; }
        public String CreatedDate { get; set; }
        public String CustomerName { get; set; }
        public String CustomerContact { get; set; }
        public String CustomerEmail { get; set; }
        public String BuildiingAddress { get; set; }
        public List<String> WorkscopeName { get; set; }
        public String Amount { get; set; }
        public String Discount { get; set; }
        public String VAT { get; set; }
        public String Deduction { get; set; }
        public String TotalAmount { get; set; }
        public decimal? AmounttoBePaid { get; set; }
        public String PaymentType { get; set; }


    }
}
