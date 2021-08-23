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
        public List<Calendar> calendar { get; set; }

    }

    public class OldCalendar
    {
        public int InquiryId { get; set; }
        public int InquiryWorkscopeId { get; set; }
        public string InquiryCode { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string DesignScheduledate { get; set; }
        public string WorkscopeName { get; set; }
        public int InquiryworkscopeStatusId { get; set; }
    }
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string OnClickURL { get; set; }
        public int EventTypeId { get; set; }
    }
}
