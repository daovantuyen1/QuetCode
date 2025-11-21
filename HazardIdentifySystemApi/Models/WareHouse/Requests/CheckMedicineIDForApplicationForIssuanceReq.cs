using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse.Requests
{
    public class CheckMedicineIDForApplicationForIssuanceReq
    {
        public string MedicineID { set; get; }
        public string FactoryZone { set; get; }
    }
}