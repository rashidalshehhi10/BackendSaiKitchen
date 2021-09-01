using System.Collections.Generic;

namespace BackendSaiKitchen.CustomModel
{
    public class CustomMeasurement
    {
        public int MeasurementId { get; set; }
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

        public int Wdiid { get; set; }
        public string WdiclosetType { get; set; }
        public string WdiboardModel { get; set; }
        public string WdiselectedColor { get; set; }
        public string WdiceilingHeight { get; set; }
        public string WdiclosetHeight { get; set; }
        public bool? WdistorageDoor { get; set; }
        public string WdidoorDesign { get; set; }
        public string WdihandleDesign { get; set; }
        public string WdidoorMaterial { get; set; }
        public string Wdinotes { get; set; }

        public string MeasurementComment { get; set; }

        public List<string> base64img { get; set; }
        public List<CustomAccossries> Accesories { get; set; }



    }

    public class CustomAccossries
    {
        public int AccesoriesId { get; set; }
        public int? WardrobeDesignInfoId { get; set; }
        public string AccesoriesName { get; set; }
        public bool? AccesoriesValue { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}