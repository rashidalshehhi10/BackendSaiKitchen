using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public class MeasurementDetail
    {
        public MeasurementDetail()
        {
            MeasurementDetailInfos = new HashSet<MeasurementDetailInfo>();
            Measurements = new HashSet<Measurement>();
        }

        public int MeasurementDetailId { get; set; }
        public string MeasurementDetailName { get; set; }
        public string MeasurementDetailDescription { get; set; }
        public string MeasurementDetailCeilingHeight { get; set; }
        public string MeasurementDetailCielingDiameter { get; set; }
        public string MeasurementDetailCornishHeight { get; set; }
        public string MeasurementDetailCornishDiameter { get; set; }
        public string MeasurementDetailSkirtingHeight { get; set; }
        public string MeasurementDetailSkirtingDiameter { get; set; }
        public string MeasurementDetailPlinthHeight { get; set; }
        public string MeasurementDetailPlinthDiameter { get; set; }
        public string MeasurementDetailDoorHeight { get; set; }
        public string MeasurementDetailSpotLightFromWall { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public virtual ICollection<MeasurementDetailInfo> MeasurementDetailInfos { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
    }
}
