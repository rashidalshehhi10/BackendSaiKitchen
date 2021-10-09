﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class Fee
    {
        public Fee()
        {
            Payments = new HashSet<Payment>();
        }

        public int FeesId { get; set; }
        public string FeesName { get; set; }
        public string FeesDescription { get; set; }
        public string FeesAmount { get; set; }
        public bool? IsPercentage { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
