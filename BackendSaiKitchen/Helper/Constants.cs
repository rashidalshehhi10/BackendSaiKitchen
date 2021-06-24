using System;

namespace BackendSaiKitchen.Helper
{
    public class Constants
    {
        public static string CRMBaseUrl = "https://saikitchen.azurewebsites.net";
        public static string AzureUrl = "https://saikitchenstorage.blob.core.windows.net/files/";
        public static string VimeoUrl = "https://vimeo.com/";
        public static string VimeoAccessToken = "30fd2e2feeccc9b6752294a38ec950e5";
        public static string loginErrormessage = "Incorrect Email or Password";
        public static string inquiryOnAnotherBranchMessage = " generated inquiry on another branch";
        public static string measurementRescheduleBranchMessage = " measurement reschedule to ";
        public static string measurementAssign = " You are assigned for the new measurement at ";
        public static string wrongFileUpload = "Kindly upload jpg,png or PDF";
        public static string MeasurementFileMissing = "Measurement File missing";
        public static string DesignVideoFileMissing = "Design Video File Missing";
        public static string QuotationFileMissing = "Quotation File Missing";
        public static string ContractFileMissing = "Contract File Missing";
        public static string MeasurementMissing = "Measurement doesnt Exist";
        public static string DesginMissing = "Design doesnt Exist";
        public static string QuotationMissing = "Quotation Doesnt Exist";
        public static string MeasurementDelayed = " Delayed the Measurement";
        public static string MeasurementAdded = "New Measurement added";
        public static string DesignAssign = "You assign for new Design";
        public static string DesignDelayed = "Delayed the Design";
        public static string DesignAdded = "New Design added";
        public static int userId;
        public static int userRoleId;
        public static int branchId;
        public static int branchRoleId;
        public static string userToken;

        public static string InquiryEmailResponse(string CustomerName, String InquiryCode)
        {
            return "";
        }
    };


    public enum permission
    {
        ManageBranch = 2,
        ManageBranchRole = 3,
        ManageUsers = 4,
        ManageCustomer = 5,
        ManageInquiry = 6,
        ManageMeasurement = 7,
        ManageDesign = 8,
        ManageQuotation = 9
    }
    public enum permissionLevel
    {
        Read = 1,
        Create = 2,
        Update = 3,
        Escalate = 4,
        Delete = 5
    }
    public enum inquiryStatus
    {
        measurementPending = 1,
        measurementdelayed = 2,
        designPending = 3,
        designDelayed = 4,
        quotationPending = 5,
        quotationDelayed = 6,
        measurementAccepted=7,
        measurementRejected=8,
        measurementWaitingForApproval=9,
        designAccepted = 10,
        designRejected = 11,
        designWaitingForApproval = 12,
        quotationAccepted = 13,
        quotationRejected = 14,
        quotationWaitingForApproval = 15,
        designWaitingForCustomerApproval = 16,
        designRejectedByClient = 17,

    }

    public enum roleType
    {
        Manager = 1,
        Accounts = 2,
        Sales = 3,
        Designer = 4,
        Procurement = 5
    }

    public enum notificationCategory
    {
        Inquiry = 1,
        Measurement = 2,
        Design = 3,
        Quotation = 4,
        JobOrder = 5,
        Procurement = 6,
        Supplier = 7,
        Delivery = 8,
        Other = 9
    }


}
public class ServiceResponse
{
    public bool isError = false;
    public String errorMessage = "";
    public Object data;
}
public class TableResponse
{
    public int recordsTotal = 0;
    public int recordsFiltered = 0;
    public Object data;
}
