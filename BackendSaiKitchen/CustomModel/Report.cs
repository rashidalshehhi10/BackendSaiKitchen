using System.Collections.Generic;

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

    public class BranchReport
    {
        public decimal? AmountReceived { get; set; }
        public decimal? AmountPending { get; set; }
        public int? InquiriesCompleted { get; set; }
        public int? InquiriesInComplete { get; set; }
        public int? QuotationAccepted { get; set; }
        public int? QuotationPending { get; set; }
        public int? QuotationRejected { get; set; }
        public int? JobOrderCreated { get; set; }
        public int? JobOrederRejected { get; set; }
        public int? CashPaid { get; set; }
        public decimal? TotalCash { get; set; }
        public int? ChequePaid { get; set; }
        public decimal? TotalCheque { get; set; }
        public int? OnlinePaid { get; set; }
        public decimal? TotalOnline { get; set; }
        public int? BankPaid { get; set; }
        public decimal? TotalBank { get; set; }
        public double? CustomerSatisfy { get; set; }
        public List<TopFivePaidCustomer> topFivePaidCustomers { get; set; }
        public List<TopFiveNewCustomers> TopFiveNewCustomers { get; set; }
        public List<InquiryReceivedDetails> InquiryReceivedDetails { get; set; }
        public List<InquiryPendingDetails> inquiryPendingDetails { get; set; }
        public List<Employee> employees { get; set; }
        public List<ReceivedPaymentMode> receivedPaymentModes { get; set; }
        public List<CustomerContactSource> customerContactSources { get; set; }
        public List<MonthlyReview> CustomerSatisfaction { get; set; }
        public List<MonthlyReview> MonthlyAmountReceived { get; set; }
    }

    public class MonthlyReview
    {
        public string Month { get; set; }
        public decimal? Avarege { get; set; }
    }

    public class ReceivedPaymentMode
    {
        public string PaymentMode { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class CustomerContactSource
    {
        public string ContactMode { get; set; }
        public decimal? Percentage { get; set; }
    }

    public class TopFivePaidCustomer
    {
        public string Name { get; set; }
        public decimal AmountRecieved { get; set; }
        public string CustomerContact { get; set; }
    }

    public class TopFiveNewCustomers
    {
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string CustomerContact { get; set; }
    }

    public class UserReport
    {
        public int CustomerCount { get; set; }
        public List<Review> reviews { get; set; }
    }

    public class InquiryReceivedDetails
    {
        public string InquiryCode { get; set; }
        public List<string> WorkscopeName { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string MobileNomber { get; set; }

        public string Email { get; set; }

        //public string PaymentMethodId { get; set; }
        //public string PaymentType { get; set; }
        //public string PaymentMode { get; set; }
        public decimal AmountRecieved { get; set; }

        //public string Date { get; set; }
        public List<reportPayment> reportPayments { get; set; }
    }

    public class reportPayment
    {
        public string PaymentMethod { get; set; }
        public string PaymentType { get; set; }
        public int PaymentModeId { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
    }

    public class InquiryPendingDetails
    {
        public string InquiryCode { get; set; }
        public List<string> WorkscopeName { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string MobileNomber { get; set; }
        public string Email { get; set; }

        public decimal AmountPending { get; set; }

        //public string DueDate { get; set; }
        public List<reportPayment> reportPayments { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
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