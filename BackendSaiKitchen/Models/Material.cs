﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class Material
    {
        public Material()
        {
            Sizes = new HashSet<Size>();
        }

        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Size> Sizes { get; set; }
    }
}
