using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse
{
    public class ImportGoodsToWareHouseRq
    {
        public int Row { set; get; }  // order row of excel
        public string medicineID { set; get; }
        public string medicineName { set; get; }
        public string medicineNameVi { set; get; }
        public string medicineNameEn { set; get; }
        public string systemInputDate { set; get; }
        public string realInputDate { set; get; }
        public int inQty { set; get; }
        public string totalQty { set; get; }
        public string medicineExpiredDate { set; get; }
        public string buyDocNum { set; get; }
        public string medicineObjUnit { set; get; }
        public string medicineObjUnitVi { set; get; }
        public string medicineObjUnitEn { set; get; }
        public string factoryZonee { set; get; }
        public string inputType { set; get; }
        public string returnNote { set; get; }
        

    }

   
}