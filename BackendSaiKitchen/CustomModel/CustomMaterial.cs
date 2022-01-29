using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomMaterial
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImg { get; set; }
        public string Skucode { get; set; }
        public int UnitOfMeasurementId { get; set; }
        public List<string> SizeDetail { get; set; }
        public int WorkscopeId { get; set; }
    }
}
