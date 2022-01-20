using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class PurchaseOrderCustomModel
    {
        public int inquiryId { get; set; }
        public List<Order> lpo { get; set; }
    }

    public class Order
    {
        public string PurchaseOrderAmount { get; set; }
        public string PurchaseOrderExpectedDeliveryDate { get; set; }
        public List<string> files { get; set; }
    }

    public class UpdateExpectedDate
    {
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderExpectedDeliveryDate { get; set; }
    }
}
