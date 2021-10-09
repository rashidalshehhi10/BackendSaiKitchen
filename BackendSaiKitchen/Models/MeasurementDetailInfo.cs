using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class MeasurementDetailInfo
    {
        public int MeasurementDetailInfoId { get; set; }
        public string MeasurementDetailInfoName { get; set; }
        public string MeasurementDetailInfoDistanceHeight { get; set; }
        public string MeasurementDetailInfoDistanceLl { get; set; }
        public string MeasurementDetailInfoDistanceRr { get; set; }
        public string MeasurementDetailInfoDistanceHff { get; set; }
        public int? MeasurementDetailId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual MeasurementDetail MeasurementDetail { get; set; }
    }
}
