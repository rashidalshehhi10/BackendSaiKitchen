using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class Dashborad
    {
        public int CustomerRegistered { get; set; }
        public int InquiryCompleted { get; set; }
        public int CustomerContacted { get; set; }
        public int CustomerNeedtoContact { get; set; }
        public int TotalInquiries { get; set; }
        public int Totalquotations { get; set; }
        public int QuotationAccepted { get; set; }
        public int QuotationRejected { get; set; }
        public int TotalJoborder { get; set; }
        public int InquiryIncomplete { get; set; }
        //public int Invoicegenerated { get; set; }
        //public int InvoicePaid { get; set; }
        //public int TotalCashPayment { get; set; }
        //public int TotalChequePayment { get; set; }
        //public int TotalOnlinePayment { get; set; }
        //public List<string> MeasurementScheduleDate { get; set; }
        //public List<string> DesignScheduledate { get; set; }
        //public List<string> InquiryCode { get; set; }
        //public List<string> WorkscopeName { get; set; }
        public List<Calendar> calendar { get; set; }

    }

    public class Calendar
    {
        public int InquiryId { get; set; }
        public int InquiryWorkscopeId { get; set; }
        public string InquiryCode { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string DesignScheduledate { get; set; }
        public string WorkscopeName { get; set; }
        public int InquiryworkscopeStatusId { get; set; }
    }
}
