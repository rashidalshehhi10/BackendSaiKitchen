using BackendSaiKitchen.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<Accesory> accesories { get; set; }
        public List<File> files { get; set; }

        public int? DesignAssignedTo { get; set; }
        public string DesignScheduleDate { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

    }
}
