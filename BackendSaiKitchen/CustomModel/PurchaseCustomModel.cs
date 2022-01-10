using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class PurchaseCustomModel
    {
        public int inquiryId { get; set; }
        public List<Request> lpo { get; set; }
    }

    public class Request
    {
        //public string PurchaseRequestTitle { get; set; }
        //public string PurchaseRequestDescription { get; set; }
        public string PurchaseRequestFinalDeliveryRequestedDate { get; set; }
        public List<string> files { get; set; }
    }
}
