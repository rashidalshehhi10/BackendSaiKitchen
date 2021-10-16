using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class ViewQuotation
    {
        public string InvoiceNo { get; set; } // "QTN" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId +""+quotation.QuotationId;
        public string CreatedDate { get; set; }
        public string ValidDate { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }//promoCode
        public string MeasurementFee { get; set; }
        public string Amount { get; set; }
        public string Vat { get; set; }
        public string MeasurementFees { get; set; }//payment.p
        public string AdvancePayment { get; set; }
        public string BeforeInstallation { get; set; }
        public bool? IsInstallment { get; set; }
        public string ProposalReferenceNumber { get; set; }
        public List<Payment> installments { get; set; }
        public string AfterDelivery { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerWhatsapp { get; set; }
        public string BuildingAddress { get; set; }
        public string BranchAddress { get; set; }
        public string BranchContact { get; set; }
        public List<TermsAndCondition> TermsAndConditionsDetail { get; set; }
        public List<string> inquiryWorkScopeNames { get; set; }
        public List<int> Quantity { get; set; } //inqruiryWorkScope.workscopesId == 1.count
        public List<InvoiceDetail> invoiceDetails { get; set; }
        public List<string> Files { get; set; }
        public string TotalAmount { get; set; }
        public string QuotationScheduleDate { get; set; }
    }
    public class InvoiceDetail
    {

        public String inquiryWorkScopeNames { get; set; }
        public int Quantity { get; set; } //inqruiryWorkScope.workscopesId == 1.count
    }

    public class UpdateQuotationStatus
    {
        public int inquiryId { get; set; }
        public string reason { get; set; }
        public int FeedBackReactionId { get; set; }
        //public string PaymentIntentToken { get; set; }
        //public string ClientSecret { get; set; }
        //public string PaymentMethod { get; set; }
        //public int SelectedPaymentMode { get; set; }
        public byte[] Pdf { get; set; }
    }

   

    public class quotationScheduleUpdate
    {
        public int inquiryId { get; set; }
        public string date { get; set; }
        public int userId { get; set; }
    }

    public class GetfeesForQuotation
    {
        public Inquiry inquiry { get; set; }
        public List<Fee> fees { get; set; }
    }
}
