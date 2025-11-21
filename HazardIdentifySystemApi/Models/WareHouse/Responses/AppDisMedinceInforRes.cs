using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.WareHouse.Responses
{
    public class AppDisMedinceInforRes
    {
        public TB_APP_DIS_MEDICINE AppDisMedince { set; get; }
        public List<V_TB_APP_DIS_MEDICINE_DETAIL> AppDisMedinceDetailLs { set; get; }
    }
}