using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class V_APP_DIS_MEDICINE
    {
        public TB_APP_DIS_MEDICINE tbAppDisMedince { set; get; }
        public List<TB_APP_DIS_MEDICINE_DETAIL> tbAppDisMedinceDetailLs { set; get; }
    }
}