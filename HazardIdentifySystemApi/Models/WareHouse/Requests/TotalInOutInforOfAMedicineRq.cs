using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse
{
    public class TotalInOutInforOfAMedicineRq
    {
        public string medicineId { set; get; }
        public string medicineExpiredDate { set; get; }
        public string factoryZone { set; get; }
    }
}