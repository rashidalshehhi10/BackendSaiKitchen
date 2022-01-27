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
        public int WorkscopeId { get; set; }
    }
}
