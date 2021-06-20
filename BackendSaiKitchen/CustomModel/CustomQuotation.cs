using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomQuotation
    {

        public int QuotationId { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public int? InquiryId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string Description { get; set; }
        public string TotalAmount { get; set; }
        public string AdvancePayment { get; set; }
        public string Discount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        public List<byte[]> ContractFiles { get; set; }
        public List<byte[]> QuotationFiles { get; set; }
    }

    public class QuotationFile
    {
        public int Ininquiryworkscopeid { get; set; }
        public string Comment { get; set; }


        public List<byte[]> base64img { get; set; }
    }
    public class ContractFile
    {
        public int Ininquiryworkscopeid { get; set; }
        public string Comment { get; set; }


        public List<byte[]> base64img { get; set; }
    }
}
