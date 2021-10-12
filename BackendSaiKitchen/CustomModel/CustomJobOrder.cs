using BackendSaiKitchen.Models;
using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomJobOrder
    {
        public int inquiryId { get; set; }
        public string materialRequestDate { get; set; }
        public string shopDrawingCompletionDate { get; set; }
        public string productionCompletionDate { get; set; }
        public string woodenWorkCompletionDate { get; set; }
        public string materialDeliveryFinalDate { get; set; }
        public string counterTopFixingDate { get; set; }
        public string installationStartDate { get; set; }
        // public string installationEndDate { get; set; }
        public string Notes { get; set; }

        public int PaymentTypeId { get; set; }
        public bool? IsInstallment { get; set; }
        public int? NoOfInstallment { get; set; }
        public string AdvancePayment { get; set; }
        public string BeforeInstallation { get; set; }
        public string AfterDelivery { get; set; }
        //public string QuotationValidityDate { get; set; }
        public byte[] Pdf { get; set; }
        public List<Payment> Payments { get; set; }

    }

    public class JobOrderFactory
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
