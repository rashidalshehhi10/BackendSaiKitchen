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
        public  int status { get; set; }

        // public string installationEndDate { get; set; }
        public string Notes { get; set; }
    }

    public class ContrcatApprove
    {
        public int inquiryId { get; set; }
        public string Comment { get; set; }
        public int FeedBackReactionId { get; set; }
        public string PaymentIntentToken { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentMethod { get; set; }
        public int SelectedPaymentMode { get; set; }
        public byte[] Pdf { get; set; }
    }

    public class ContractReject
    {
        public int inquiryId { get; set; }
        public string Comment { get; set; }
        public int FeedBackReactionId { get; set; }
    }


    public class AddjobOrder
    {
        public int inquiryId { get; set; }

        //public string JobOrderRequestedDeadline { get; set; }
        //public string JobOrderRequestedComments { get; set; }
        //public string DataSheetApplianceFileUrl { get; set; }
        //public bool IsAppliancesProvidedByClient { get; set; }
        //public string DetailedDesignFile { get; set; }
        //public string MaterialSheetFileUrl { get; set; }

        //public string MepdrawingFileUrl { get; set; }
        //public string Comments { get; set; }

        public int PaymentTypeId { get; set; }
        public bool? IsInstallment { get; set; }
        public int? NoOfInstallment { get; set; }
        public string AdvancePayment { get; set; }
        public string BeforeInstallation { get; set; }
        public string AfterDelivery { get; set; }
        public byte[] Pdf { get; set; }
        public List<string> signedquotation { get; set; }
        public List<string> signedcontract { get; set; }
        public List<string> signeddesign { get; set; }
        public List<Payment> Payments { get; set; }
    }

    public class ContractFiles
    {
        public int inquiryId { get; set; }
        public string DataSheetApplianceFileUrl { get; set; }
        public bool IsAppliancesProvidedByClient { get; set; }
        public string DetailedDesignFile { get; set; }
        public string MaterialSheetFileUrl { get; set; }

        public string MepdrawingFileUrl { get; set; }
    }

    public class JobOrderFactory
    {
        public int inquiryId { get; set; }
        public string Reason { get; set; }
        public string base64f3d { get; set; }
    }

    public class Install
    {
        public int inquiryId { get; set; }
        public bool YesNo { get; set; }
        public string Remark { get; set; }
        public string JobOrderDetailsDescription { get; set; }
        public string installationStartDate { get; set; }
    }

    public class Factroy
    {
        public int inquiryId { get; set; }
        public int FactoryId { get; set; }
    }
}