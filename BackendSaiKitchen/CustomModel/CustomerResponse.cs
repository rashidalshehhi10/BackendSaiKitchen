namespace BackendSaiKitchen.CustomModel
{
    public class CustomCustomer
    {
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerNotes { get; set; }
        public int ContactStatusId { get; set; }
        public int WayofContactId { get; set; }
    }

    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerWhatsapp { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerNotes { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerNationalId { get; set; }
        public string CustomerNextMeetingDate { get; set; }
        public int? ContactStatusId { get; set; }
        public string? ContactStatus { get; set; }
        public int? WayofContactId { get; set; }
        public string WayofContact { get; set; }
        public int? TotalCustomers { get; set; }
        public int? ContactedCustomers { get; set; }
        public int? NeedToContactCustomers { get; set; }
        public int? CustomerWithoutInquiry { get; set; }
        public int? Direct { get; set; }
        public int? Google { get; set; }
        public int? FaceBook { get; set; }
        public int? Linkedin { get; set; }
        public int? Twitter { get; set; }
        public int? Friends { get; set; }
        public int? Website { get; set; }
        public int? MobileApp { get; set; }
        public int? OwnerReference { get; set; }
        public int? Instagram { get; set; }
        public int? Other { get; set; }
        public string? TotalNoOfInquiries { get; set; }
        public string AddedOn { get; set; }
        public string CustomerAssignedToName { get; set; }
        public int? CustomerAssignedTo { get; set; }
        public string CustomerAssignedByName { get; set; }
        public int? CustomerAssignedBy { get; set; }
        public string CustomerAssignedDate { get; set; }



        public int UserId { get; set; }
        public string UserName { get; set; }
        public int LastUserId { get; set; }
        public string LastUserName { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
    }
}