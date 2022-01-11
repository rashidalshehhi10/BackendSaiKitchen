namespace BackendSaiKitchen.Helper
{
    public class Constants
    {
        public static readonly string CRMBaseUrl = "https://saikitchen.azurewebsites.net";
        public static readonly string ServerBaseURL = "https://backendsaikitchen.azurewebsites.net/";
        public static readonly string AzureUrl = "https://saikitchenstorage.blob.core.windows.net/files/";
        public static readonly string VimeoUrl = "https://vimeo.com/";
        public static readonly string VimeoAccessToken = "00378b95cc11173f483f7f6b602f6790";
        public static string WhatsappAccessToken = "a27e1f9ca2347bb766f332b8863ebe9f";
        //public static string WhatsappInstanceId = "61C02C26095F3";

        public static string WhatsappInstanceId = "26095F3";
        public static readonly string loginErrormessage = "Incorrect Email or Password";
        public static readonly string inquiryOnAnotherBranchMessage = ", generated inquiry on another branch";
        public static readonly string measurementRescheduleBranchMessage = " measurement reschedule to ";
        public static readonly string designRescheduleBranchMessage = " Design reschedule to ";
        public static readonly string measurementAssign = " You are assigned for the new measurement at ";
        public static readonly string wrongFileUpload = "Kindly upload jpg,png or PDF";
        public static readonly string MeasurementFileMissing = "Measurement File missing";
        public static readonly string DesignVideoFileMissing = "Design Video File Missing";
        public static readonly string QuotationFileMissing = "Quotation File Missing";
        public static readonly string ContractFileMissing = "Contract File Missing";
        public static readonly string MeasurementMissing = "Measurement doesnt Exist";
        public static readonly string DesginMissing = "Design doesnt Exist";
        public static readonly string QuotationMissing = "Quotation Doesnt Exist";
        public static readonly string MeasurementDelayed = " Delayed the Measurement";
        public static readonly string MeasurementAssigneeDelayed = " Measuremnt Assignment Rejected by system";
        public static readonly string DesignAssigneeDelayed = " Design Assignment Rejected by system";
        public static readonly string MeasurementAdded = "New Measurement added ";
        public static readonly string PaymentAdded = "New Payment added";
        public static readonly string DesignAssign = "You assign for new Design at ";
        public static readonly string DesignDelayed = " Delayed the Design";
        public static readonly string QuotationDelayed = " Delayed the Quotation";
        public static readonly string JobOrderDelayed = " Job Order Has Been Delayed";
        public static readonly string DesignAdded = "New Design added For Inquiry Code: IN";

        public static readonly string UnAuthorizedUser =
            "You are not authorized to perform this action, Kindly contact the admin";

        public static int userId;
        public static int userRoleId;
        public static int branchId;
        public static int branchRoleId;
        public static string userToken;

        public static readonly int PageSize=10;

        public static string InquiryEmailResponse(string CustomerName, string InquiryCode)
        {
            return "";
        }
    }


    public enum contactStatus
    {
        Contacted = 1,
        NeedToContact = 2,
        NeedToFollowUp = 3,
        NotResponing = 4,
    }

    public enum eventType
    {
        Customer = 1,
        Inquiry = 2,
        Measurement = 3,
        Design = 4,
        Quotation = 5,
        Checklist = 6,
        JobOrder = 7,
        Payment = 8,
        Installation = 9,
        Delivery = 10,
        Promo = 11,
        Other = 12,
        MeasurementAssign = 13,
        DesignAssign = 14,
        ShopDrawing = 15,
        Production = 16,
        WoodenWork = 17,
        CounterTopFixing = 18,

    }

    public enum permission
    {
        ManageBranch = 2,
        ManageBranchRole = 3,
        ManageUsers = 4,
        ManageCustomer = 5,
        ManageInquiry = 6,
        ManageMeasurement = 7,
        ManageDesign = 8,
        ManageQuotation = 9,
        ManageWorkscope = 10,
        ManageFees = 11,
        ManagePromo = 12,
        ManageChecklist = 13,
        ManageJobOrder = 14,
        ManagePayment = 15
    }

    public enum permissionLevel
    {
        Read = 1,
        Create = 2,
        Update = 3,
        Escalate = 4,
        Delete = 5
    }

    public enum purchaseStatus
    {
        purchaseRequested = 1,
        purchaseOrdered = 2,
        purchaseOrderedDelayed =3,
        purchaseDelivered = 4
    }

    public enum inquiryStatus
    {
        measurementInProgress = 1,
        measurementdelayed = 2,
        designPending = 3,
        designDelayed = 4,
        quotationPending = 5,
        quotationDelayed = 6,
        measurementAccepted = 7,
        measurementRejected = 8,
        measurementWaitingForApproval = 9,
        designAccepted = 10,
        designRejected = 11,
        designWaitingForApproval = 12,
        quotationAccepted = 13,
        quotationRejected = 14,
        quotationWaitingForCustomerApproval = 15,
        designWaitingForCustomerApproval = 16,
        designRejectedByCustomer = 17,
        checklistPending = 18,
        checklistAccepted = 19,
        checklistRejected = 20,
        jobOrderPending = 21,
        jobOrderCreated = 22,
        jobOrderApproved = 23,
        jobOrderRejected = 24,
        jobOrderWaitingForApproval = 25,
        jobOrderDelayed = 26,
        jobOrderCompleted = 27,
        deliveryPending = 28,
        unabletoDeliver = 29,
        delivered = 30,
        waitingForMeasurmenetFees = 31,
        waitingForAdvance = 32,
        waitingForBeforeDeliveryPayment = 33,
        waitingForAfterDeliveryPayment = 34,
        inquiryCompleted = 35,
        measurementAssigneePending = 36,
        measurementAssigneeAccepted = 37,
        measurementAssigneeRejected = 38,
        designAssigneePending = 39,
        designAssigneeAccepted = 40,
        designAssigneeRejected = 41,
        quotationSchedulePending = 42,
        commercialChecklistPending = 43,
        commercialChecklistAccepted = 44,
        commercialChecklistRejected = 45,
        jobOrderConfirmationPending = 46,
        jobOrderInProgress = 47,
        jobOrderFactoryRejected = 48,
        jobOrderRescheduleRequested = 49,
        jobOrderRescheduleApproved = 50,
        jobOrderRescheduleRejected = 51,
        jobOrderDelayRequested = 52,
        jobOrderReadyForInstallation = 53,
        jobOrderFilesPending = 54,
        jobOrderFilesDelayed = 55,
        designRevisionRequested = 56,
        quotationWaitingForApproval = 57,
        contractPending = 58,
        contractWaitingForCustomerApproval = 59,
        contractApproved = 60,
        quotationRevisionRequested = 61,
        contractInProgress = 62,
        contractRejected = 63,
        jobOrderAuditPending = 64,
        jobOrderAuditRejected = 65,
        specialApprovalPending = 66,
        specialApprovalRejected = 67,
        jobOrderFactoryDelayed = 68,
        jobOrderProcurementDelayed = 69,
        jobOrderCustomerRequestReschedule = 70,
        jobOrderDesignTeamDelay = 71,
        DetailedFilesPending = 72,
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

    public enum paymentstatus
    {
        PaymentCreated = 1,
        PaymentPending = 2,
        PaymentWaitingofApproval = 3,
        PaymentApproved = 4,
        PaymentRejected = 5,
        InstallmentCreated = 6,
        InstallmentPending = 7,
        InstallmentWaitingofApproval = 8,
        InstallmentApproved = 9,
        InstallmentRejected = 10
    }

    public enum paymenttype
    {
        Measurement = 1,
        AdvancePayment = 2,
        BeforeInstallation = 3,
        AfterDelivery = 4,
        Installment = 5,
        EngineeringFees = 6,
        ExtraCharges = 7
    }

    public enum newsletterFrequency
    {
        OneTime = 1,
        Daily = 2,
        Weekly = 3,
        BiWeekly = 4,
        Monthly = 5,
        Annually = 6
    }

    public enum paymentMode
    {
        Cash = 1,
        Cheque = 2,
        OfflinePaybyCard = 3,
        OnlinePayment = 4
    }

    public enum FeedBackReaction
    {
        Exhausted = 1,
        Angery = 2,
        Sad = 3,
        Neutral = 4,
        Satisfied = 5,
        Cool = 6,
        Blessed = 7
    }
}

public class ServiceResponse
{
    public object data;
    public string errorMessage;
    public bool isError;
}

public class TableResponse
{
    public object data;
    public int recordsFiltered;
    public int recordsTotal;
}