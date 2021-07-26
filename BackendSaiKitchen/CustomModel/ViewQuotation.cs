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
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public List<string> Quantity { get; set; } //inqruiryWorkScope.workscopesId == 1.count
        public string Discount { get; set; }//promoCode
        public string Amount { get; set; }
        public string Vat { get; set; }
        public string MeasurementFees { get; set; }//payment.paymnettypeid ==1.measurement.amount 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string BuildingAddress { get; set; }

        public List<string> inquiryWorkScopeNames { get; set; }


    }

    public class UpdateQuotationStatus
    {
        public int inquiryId { get; set; }
        public string reason { get; set; }
    }
}
