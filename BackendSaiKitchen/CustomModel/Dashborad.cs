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
        public ICollection<InquiryWorkscope>Scheduledate { get; set; }
    }
}
