using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomJobOrder
    {
        public int inquiryId { get; set; }
        public string materialAvailablityDate { get; set; }
        public string shopDrawingCompletionDate { get; set; }
        public string productionCompletionDate { get; set; }
        public string woodenWorkCompletionDate { get; set; }
        public string materialDeliveryFinalDate { get; set; }
        public string counterTopFixingDate { get; set; }
        public string installationStartDate { get; set; }
       // public string installationEndDate { get; set; }
        public string Notes { get; set; }
    }

    public class JobOrderFactoryReject
    {
        public int inquiryId { get; set; }
        public string Reason { get; set; }
    }

    public class Install
    {
        public int inquiryId { get; set; }
        public bool YesNo { get; set; }
        public string Remark { get; set; }
        public string JobOrderDetailsDescription { get; set; }
        public string installationStartDate { get; set; }
    }
}
