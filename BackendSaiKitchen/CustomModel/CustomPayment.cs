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

    public class beforeQuotation
    {
        public int inquiryId { get; set; }
        public int paymentModeId { get; set; }
        public List<string> files { get; set; }
        public double amount { get; set; }
    }

     public class addPayment
    {
        public int paymentId { get; set; }
        public string PaymentIntentToken { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentMethod { get; set; }
        public int SelectedPaymentMode { get; set; }
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
        public int userId { get; set; }
        public List<string> Files { get; set; }
    }

    public class SalesInvoiceReciept
    {
        public string InvoiceCode { get; set; }
        public string InquiryCode { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmail { get; set; }
        public string BuildiingAddress { get; set; }
        public List<string> WorkscopeName { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string VAT { get; set; }
        public string Deduction { get; set; }
        public string TotalAmount { get; set; }
        public decimal? AmounttoBePaid { get; set; }
        public string PaymentType { get; set; }
    }
}