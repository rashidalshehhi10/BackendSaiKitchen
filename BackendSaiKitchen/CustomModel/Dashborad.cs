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
        public List<InquiryWorkscope> MeasurementScheduleDate { get; set; }
        public List<InquiryWorkscope> DesignScheduledate { get; set; }
        public List<string> InquiryCode { get; set; }
        public List<string> WorkscopeName { get; set; }
        public List<Calendar> calendar { get; set; }

    }

    public class Calendar
    {
        public string InquiryCode { get; set; }
        public string MeasurementScheduleDate { get; set; }
        public string DesignScheduledate { get; set; }
        public string WorkscopeName { get; set; }
    }
}
