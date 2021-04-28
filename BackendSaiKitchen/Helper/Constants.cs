﻿using System;

namespace BackendSaiKitchen.Helper
{
    public class Constants
    {
        public static string CRMBaseUrl = "http://localhost:8080";
        public static string loginErrormessage = "Incorrect Email or Password";
        public static string inquiryOnAnotherBranchMessage = " generated inquiry on another branch";
        public static string measurementRescheduleBranchMessage = " measurement reschedule to " ;
        public static string measurementAssign = " You are assigned for the new measurement at ";
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
        quotationDelayed = 6
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
