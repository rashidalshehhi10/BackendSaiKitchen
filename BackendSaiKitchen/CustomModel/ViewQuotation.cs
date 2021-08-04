using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class ViewQuotation
    {
        public string InvoiceNo { get; set; } // "INV" + inquiry.BranchId + "" + inquiry.CustomerId + "" + inquiry.InquiryId +""+quotation.QuotationId;
        public string CreatedDate { get; set; }
        public string ValidDate { get; set; }
        public string Description { get; set; }
        public string Discount { get; set; }//promoCode
        public string Amount { get; set; }
        public string Vat { get; set; }
        public string MeasurementFees { get; set; }//payment.paymnettypeid ==1.measurement.amount 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string BuildingAddress { get; set; }
        public string BranchAddress { get; set; }
        public string BranchContact { get; set; }
        public List<TermsAndCondition> TermsAndConditionsDetail { get; set; }
        public List<string> inquiryWorkScopeNames { get; set; }
        public List<int> Quantity { get; set; } //inqruiryWorkScope.workscopesId == 1.count
        public List<InvoiceDetail> invoiceDetails { get; set; }
        public ICollection<File> Files { get; set; }
        public string TotalAmount { get; set; }
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

        public string PaymentIntentToken { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class GetfeesForQuotation
    {
        public Inquiry inquiry { get; set; }
        public List<Fee> fees { get; set; }
    }
}
