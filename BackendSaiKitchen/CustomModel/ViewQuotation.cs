﻿using System;
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
        public int Quantity { get; set; } //inqruiryWorkScope.workscopesId == 1.count
        public decimal Discount { get; set; }//promoCode
        public decimal Amount { get; set; }
        public decimal Vat { get; set; }
        public decimal MeasurementFees { get; set; }//payment.paymnettypeid ==1.measurement.amount 
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string BuildingAddress { get; set; }

        public List<string> inquiryWorkScopeNames { get; set; }


    }
}
