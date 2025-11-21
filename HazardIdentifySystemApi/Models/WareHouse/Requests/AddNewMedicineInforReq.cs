using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse.Requests
{
    public class AddNewMedicineInforReq
    {
        public string medicineId { set; get; }
        public string medicineNameVi { set; get; }
        public string medicineNameEn { set; get; }
        public string medicineObjUnitVi { set; get; }
        public string medicineObjUnitEn { set; get; }
    }

}