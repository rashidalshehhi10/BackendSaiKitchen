using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomCommercialProject
    {
        public string jobno { get; set; }
        public string projectname { get; set; }
        public string location { get; set; }
        public List<apartment> excel { get; set; }
        public List<CustomProjectDetail> scopeofWork { get; set; }
    }

    public class CustomProjectDetail
    {
        public int MaterialId { get; set; }
        public int SizeId { get; set; }
        public int WorkScopeId { get; set; }
        public int Quantity { get; set; }
    }

    public class apartment
    {
        public string apartmentname { get; set; }
    }
}
