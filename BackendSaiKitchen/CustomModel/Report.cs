using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class Report
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int QuotationPending { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalUnpaid { get; set; }
        public decimal TotalPaid { get; set; }
        public int CashPaid { get; set; }
        public decimal TotalCash { get; set; }
        public int ChequePaid { get; set; }
        public decimal TotalCheque { get; set; }
        public int OnlinePaid { get; set; }
        public decimal TotalOnline { get; set; }
        public int BankPaid { get; set; }
        public decimal TotalBank { get; set; }
        public int TotalInquiries { get; set; }
        public int Totalquotations { get; set; }
        public int QuotationAccepted { get; set; }
        public int QuotationRejected { get; set; }
        public int TotalJoborder { get; set; }
        public int Payments { get; set; }
        public List<Review> reviews { get; set; }

    }

    public class UserReport
    {
        public int CustomerCount { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class Review
    {
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public int inquiryStatusId { get; set; }
        public List<CustomerReview> customerReviews { get; set; }
    }

    public class CustomerReview
    {
        public int id { get; set; }
        public int feedBack { get; set; }
        public int statusId { get; set; }
        public string Name { get; set; }
    }

    public class ReqReport
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
