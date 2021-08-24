using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class Report
    {
        public int TotalInquiries { get; set; }
        public int Totalquotations { get; set; }
        public int QuotationAccepted { get; set; }
        public int QuotationRejected { get; set; }
        public int TotalJoborder { get; set; }
        public int Payments { get; set; }
        public List<Calendar> calendar { get; set; }

    }

    public class ReqReport
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
