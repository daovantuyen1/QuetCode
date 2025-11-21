using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class TB_MEDICINE_INFOR
    {

        public string F_MEDICINE_ID { set; get; }
        public string F_MEDICINE_NAME_VI { set; get; }
        public string F_MEDICINE_NAME_EN { set; get; }
        public string F_MEDICINE_OBJ_UNIT_VI { set; get; }
        public string F_MEDICINE_OBJ_UNIT_EN { set; get; }
        public string F_SYSDATE { set; get; }
        public string F_SYSID { set; get; }

    }

    public class V_TB_MEDICINE_INFOR : TB_MEDICINE_INFOR
    {
        public string F_SYSDATE1 { set; get; }
    }

}