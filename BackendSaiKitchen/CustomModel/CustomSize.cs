using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomSize
    {
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public string SizeDescription { get; set; }
        public decimal SizeHeight { get; set; }
        public decimal SizeWidth { get; set; }
        public int MaterialId { get; set; }
    }
}
