using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse.Requests
{
    public class ExportGoodsFromWHByNormalTypeReq
    {
        public string receiveDocNum { set; get; }
        public List<V_TB_APP_DIS_MEDICINE_DETAIL> medicineExportLs { set; get; }
    }
}